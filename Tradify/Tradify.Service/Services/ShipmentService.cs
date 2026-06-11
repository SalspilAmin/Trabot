using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tradify.Data.Entities;
using Tradify.Data.Enums;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Service.Services
{
    public class ShipmentService : Service<Shipments>, IShipmentService
    {
        private readonly ICurrentUserService currentUserService;
        private readonly ISubOrderService subOrderService;
        private readonly ApplicationDbContext context;
        private readonly ILogger<ShipmentService> logger;


        public ShipmentService(IGenericRepository<Shipments> repository, ISubOrderService subOrderService 
            ,  ICurrentUserService currentUserService
            , ApplicationDbContext context
            , ILogger<ShipmentService> logger) : base(repository)

        {
            this.subOrderService = subOrderService;
            this.currentUserService = currentUserService;
            this.context = context;
            this.logger = logger;

        }
        private string GenerateTrackingNumber()
        {
            return $"TRK-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";
        }
        public async Task<(string, int?, int?)> CreateShipment(int subOrderId)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    //  1. Check if seller exist
                    var ValidSeller = await currentUserService.GetValidSellerContextAsync();

                    if (ValidSeller.Error != null)
                        return (ValidSeller.Error, null, null);

                    // 2. Get Seller , Store
                    var seller = ValidSeller.Seller;
                    var store = ValidSeller.Store;

                    //3. Cheack If Store Type Is Product 

                    if (store.Type != StoreType.Product)

                        return ("ThisActionAllowedForProductStoresOnly", null, null);

                    //5. Check subOrder
                    var subOrder = await subOrderService.GetTableAsTracking()
                        .Include(s=>s.Order)
                        .FirstOrDefaultAsync(s=>s.Id==subOrderId
                        &&s.StoreId==store.Id);

                    if (subOrder == null)
                        return ("SubOrderNotFound", null, null);



                    // 6. Check Payment
                    if (subOrder.Order.PaymentStatus != PaymentStatus.Paid)
                    return ("OrderNotPaid", null, null);

                    // 7. Check Canceld

                    if (subOrder.Status == OrderStatus.cancelled)
                        return ("SubOrderCancelled", null, null);


                    // 8. Check already shipped
                    var alreadyShipped = await GetTableNoTracking()
                        .AnyAsync(s => s.SubOrderId == subOrder.Id);

                    if (alreadyShipped)
                    return ("AlreadyShipped", null, null);

                    // 9. Create Tracking
                 

                    var tracking = GenerateTrackingNumber();

                    while (await GetTableNoTracking()
                        .AnyAsync(s => s.TrackingNumber == tracking))
                    {
                        tracking = GenerateTrackingNumber();
                    }

                    // 10. Generate Tracking Number.
                    var shipment = new Shipments
                    {
                        TrackingNumber = tracking,
                        CurrentStatus = ShipmentStatus.Pending,
                        SubOrderId=subOrderId,  
                    };

                    var shipmentTracking = new Tradify.Data.Entities.ShipmentTracking
                    {
                        ShipmentStatus = ShipmentStatus.Pending,
                        Notes = "Shipment Created"
                    };

                    shipment.ShipmentTrackings.Add(shipmentTracking);


                    // 2. Save
                    await AddAsync(shipment);
                    await SaveChangesAsync();

                    

                    await transaction.CommitAsync();
                    return ("Success", shipment.Id, shipmentTracking.Id);
                }

                catch (Exception ex)

                {
                    await transaction.RollbackAsync();


                    logger.LogError(ex, ex.Message);
                    throw;
                }
            }

        }





        public async Task<string> UpdateShipmentStatus(int ShipmentId, ShipmentStatus status)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                // 1. Validate Seller
                var validSeller = await currentUserService.GetValidSellerContextAsync();

                if (validSeller.Error != null)
                    return validSeller.Error;

                var store = validSeller.Store;

                // 2. Get Shipment
                var shipment = await GetTableAsTracking()
                    .Include(s => s.SubOrder)
                    .ThenInclude(s => s.Order)
                    .Include(s => s.ShipmentTrackings)
                    .FirstOrDefaultAsync(s =>
                        s.Id == ShipmentId &&
                        s.SubOrder.StoreId == store.Id);

                if (shipment == null)
                    return "ShipmentNotFound";

                // 2️⃣ منع نفس الحالة
                if (shipment.CurrentStatus == status)
                    return "StatusAlreadySet";

                // 3️⃣ منع التعديل بعد النهاية
                if (shipment.CurrentStatus == ShipmentStatus.Delivered ||
                    shipment.CurrentStatus == ShipmentStatus.Returned)
                {
                    return "CannotUpdateFinalStatus";
                }
                // 3. Update Shipment Current Status
                shipment.CurrentStatus = status;
                shipment.UpdatedAt = DateTime.UtcNow;
                await UpdateAsync(shipment);
                // 4. Add Tracking History
                var tracking = new Tradify.Data.Entities.ShipmentTracking
                {
                    ShipmentId = shipment.Id,
                    ShipmentStatus = status,
                    Notes = $"Shipment Is {status}"
                };

                await context.ShipmentTracking.AddAsync(tracking);

                var suporder = await subOrderService.GetTableAsTracking().FirstOrDefaultAsync(
                    s=>s.Id == shipment.SubOrderId);

                if (suporder == null)
                    return "SuporderNotFound";

                // 5. Update SubOrder Status
                switch (status)
                {
                    case ShipmentStatus.Pending:
                        suporder.Status = OrderStatus.processing;
                        break;

                    case ShipmentStatus.Shipped:
                        suporder.Status = OrderStatus.shipped;
                        break;

                    case ShipmentStatus.Delivered:
                        suporder.Status = OrderStatus.delivered;
                        break;

                    case ShipmentStatus.Returned:
                        suporder.Status = OrderStatus.cancelled;
                        break;
                }
                 context.SubOrders.Update(suporder);

                var allSubOrders = await subOrderService.GetTableNoTracking()
                                                .Where(x => x.OrderId == suporder.OrderId)
                                                .ToListAsync();

                var mainOrderStatus = CalculateOrderStatus(allSubOrders);

                var mainOrder = await context.Orders
                    .FirstOrDefaultAsync(x => x.Id == suporder.OrderId);

                if (mainOrder != null)
                {
                    mainOrder.OrderStatus = mainOrderStatus;

                    context.Orders.Update(mainOrder);

                    await context.SaveChangesAsync();
                }


                await context.SaveChangesAsync();

                await transaction.CommitAsync();

                return "Success";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                logger.LogError(ex, ex.Message);

                throw;
            }
        }

        public OrderStatus CalculateOrderStatus (List<SubOrders> subOrders)
        {
            if (subOrders.All(x => x.Status == OrderStatus.delivered))
                return OrderStatus.delivered;

            if (subOrders.All(x => x.Status == OrderStatus.cancelled))
                return OrderStatus.cancelled;

            if (subOrders.Any(x => x.Status == OrderStatus.shipped))
                return OrderStatus.InProgress;

            if (subOrders.Any(x => x.Status == OrderStatus.delivered))
                return OrderStatus.PartiallyDelivered;

            return OrderStatus.processing;
        }
    }

}
