using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tradify.Data.Entities;
using Tradify.Data.Enums;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

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



                    // 3️⃣ Check Payment
                    if (subOrder.Order.PaymentStatus != PaymentStatus.Paid)
                    return ("OrderNotPaid", null, null);

                    if (subOrder.Status == OrderStatus.cancelled)
                        return ("SubOrderCancelled", null, null);


                    // 4️⃣ Check already shipped
                    var alreadyShipped = await GetTableNoTracking()
                        .AnyAsync(s => s.SubOrderId == subOrder.Id);

                    if (alreadyShipped)
                    return ("AlreadyShipped", null, null);

                    // 5️⃣ Create Tracking
                 

                    var tracking = GenerateTrackingNumber();

                    while (await GetTableNoTracking()
                        .AnyAsync(s => s.TrackingNumber == tracking))
                    {
                        tracking = GenerateTrackingNumber();
                    }

                    // 5️.Generate Tracking Number.
                    var shipment = new Shipments
                    {
                        TrackingNumber = tracking,
                        CurrentStatus = ShipmentStatus.Pending,
                        SubOrderId=subOrderId,  
                    };

                    var shipmentTracking = new ShipmentTracking
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
                    logger.LogError(ex, ex.Message);


                    await transaction.RollbackAsync();
                    return ("Failed", null, null);

                }
            }

        }
    }
}
