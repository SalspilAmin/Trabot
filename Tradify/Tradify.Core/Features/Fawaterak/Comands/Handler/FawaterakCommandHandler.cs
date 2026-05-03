using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Tradify.Core.Bases;
using Tradify.Core.Features.Fawaterak.Comands.Models;
using Tradify.Core.Features.Fawaterak.Comands.Validations;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Data.Enums;
using Tradify.Data.Helpers.Fawaterak;
using Tradify.Data.Helpers.Fawaterak.Einvoice;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.FawaterakServices;
using Tradify.Service.Services;
using static Tradify.Data.Helpers.Fawaterak.WebHook.CancelTransactionModel;

namespace Tradify.Core.Features.Fawaterak.Comands.Handler
{
    public class FawaterakCommandHandler : ResponseHandler,IRequestHandler<EInvoiceRequestLinkCommand,Response<EInvoiceResponseDataModel>>
   ,IRequestHandler<EnvoicePayRequestCommand,Response<BasePaymentDataResponse>>
        ,IRequestHandler<WebhookPaidCommand, Response<string>>
        , IRequestHandler<WebhookCancelCommand, Response<string>>
        , IRequestHandler<WebhookFailedCommand, Response<string>>
    {
        private readonly LocalizationService localization;
        private readonly IFawaterakServices fawaterakServices;
        private readonly IOrdersService ordersService;
        private readonly IMapper mapper;
        private readonly ICartService cartService;
        private readonly IProductService productService;
        public FawaterakCommandHandler(LocalizationService localization, IFawaterakServices fawaterak,IOrdersService ordersService,IMapper mapper
            ,ICartService cartService,IProductService productService) : base(localization)
        {
            this.localization = localization;
            this.fawaterakServices = fawaterak;
            this.ordersService = ordersService;
            this.mapper = mapper;     
            this.cartService = cartService;
            this.productService = productService;
        }

        public async Task<Response<EInvoiceResponseDataModel>> Handle(EInvoiceRequestLinkCommand requestCommnad, CancellationToken cancellationToken)
        {
            var order = await ordersService.GetByIdAsync(requestCommnad.orderId);
            var CustomerModelJsonlink = new CustomerModelJsonlink() { FirstName = requestCommnad.FirstName, LastName = requestCommnad.LastName, Email = requestCommnad.Email, Phone = requestCommnad.Phone };
            var request = new EInvoiceRequestLink(CustomerModelJsonlink) { Customer = CustomerModelJsonlink };
            request.CartItems=new List<CartItemModel> { new CartItemModel() };  
            // check of order
            if (order == null) return BadRequest<EInvoiceResponseDataModel>("NotFound");
            var listOFCartProduct = order.cart.CartProducts.ToList();
            var listOfOrderItems = order.OrderItems.ToList();
            if (listOfOrderItems == null || listOFCartProduct == null) return BadRequest<EInvoiceResponseDataModel>("NotFound");
            foreach (var orderitem in listOfOrderItems)
            {
                var product = await productService.GetByIdAsync((int)orderitem.ProductVariant.ProductId);

                var pro = mapper.Map<ProductINRequestApi>(orderitem.ProductVariant);
                var qunitity = listOFCartProduct.Where(x => x.ProductVariant.ProductId == product.Id).Select(x => x.Quantity).FirstOrDefault();
                pro.quantity = qunitity;

                request.CartItems.Add(new CartItemModel() { Name = pro.name, Price = pro.price, Quantity = (int)pro.quantity });

            }
            request.RedirectionUrls = new RedirectionUrlsModel();
            request.RedirectionUrls.OnSuccess = "https://dev.fawaterk.com/success";
            request.RedirectionUrls.OnPending = "https://dev.fawaterk.com/pending";
            request.RedirectionUrls.OnFailure = "https://dev.fawaterk.com/fail";
            var result = await fawaterakServices.CreateEInvoiceAsync(request);

            if (result != null)
            {
                return Success(result);
            }
            return BadRequest<EInvoiceResponseDataModel>(localization.Get("TryToRegisterAgain"));
        }

        public async Task<Response<BasePaymentDataResponse>> Handle(EnvoicePayRequestCommand request, CancellationToken cancellationToken)
        {
            var order = await ordersService.GetByIdAsync(request.OrderId);
            request.PayLoad.OrderId = order.Id.ToString();
            // check of order
            if (order == null) return BadRequest<BasePaymentDataResponse>("NotFound");
            var listOFCartProduct = order.cart.CartProducts.ToList();
            var listOfOrderItems = order.OrderItems.ToList();
            if (listOfOrderItems == null || listOFCartProduct == null) return BadRequest<BasePaymentDataResponse>("NotFound");
            foreach (var orderitem in listOfOrderItems)
            {
                var product = await productService.GetByIdAsync((int)orderitem.ProductVariant.ProductId);
                var pro = mapper.Map<ProductINRequestApi>(product);
                var qunitity = listOFCartProduct.Where(x => x.ProductVariant.ProductId  == product.Id).Select(x => x.Quantity).FirstOrDefault();
                pro.quantity = qunitity;

                request.CartItems.Add(new CartItemModel() { Name = pro.name, Price = pro.price, Quantity = (int)pro.quantity });

            }
            var result = await fawaterakServices.GeneralPay(request);
            if (result.Item2 == "EmailandPhoneAreNotFound") return BadRequest<BasePaymentDataResponse>(localization.Get("EmailandPhoneAreNotFound"));
            if (result.Item1 != null)
            {
                return Success<BasePaymentDataResponse>(result.Item1);
            }
            return BadRequest<BasePaymentDataResponse>(localization.Get("TryToRegisterAgain"));
        }

        public async Task<Response<string>> Handle(WebhookPaidCommand request, CancellationToken cancellationToken)
        {
           var valid = fawaterakServices.VerifyWebhook(request);
            if (!valid) return BadRequest<string>(localization.Get("UnAuthorized"));
            // we will handl it here
            var order =  ordersService.GetTableNoTracking().FirstOrDefault(x => x.invoice_id == request.InvoiceId);
            if (order == null) Success<string>("Ignore");
            if (order.PaymentStatus != PaymentStatus.Pending)
                return Success<string>("Already processed");

            order.PaymentStatus = PaymentStatus.Paid;
            await ordersService.UpdateAsync(order);
            return Success<string>(localization.Get("Success"));
        }

        public async Task<Response<string>> Handle(WebhookCancelCommand request, CancellationToken cancellationToken)
        {
            var valid = fawaterakServices.VerifyCancelTransaction(request);
            if (!valid) return BadRequest<string>(localization.Get("UnAuthorized"));

            // Handle the cancellation logic here
            // Handle the failed logic here
            var payload = request.PayLoad;
            var orderId = payload.Merchant_Reference;
            var order = await ordersService.GetByIdAsync(int.Parse(orderId));
            if (order == null) Success<string>("Ignore");
            if (order.PaymentStatus != PaymentStatus.Pending)
                return Success<string>("Already processed");

            order.PaymentStatus = PaymentStatus.Cancelled;
            await ordersService.UpdateAsync(order);

            return Success<string>(localization.Get("CanceledPaymentOperation"));

        }

        public async Task<Response<string>> Handle(WebhookFailedCommand request, CancellationToken cancellationToken)
        {
            var valid = fawaterakServices.VerifyCancelTransaction(request);
            if (!valid) return BadRequest<string>(localization.Get("UnAuthorized"));

            // Handle the failed logic here
            var payload = request.PayLoad;
            var orderId = payload.Merchant_Reference;
            var order = await ordersService.GetByIdAsync(int.Parse(orderId));
            if (order == null) Success<string>("Ignore");
            if(order.PaymentStatus!=PaymentStatus.Pending)
                return Success<string>("Already processed");

            order.PaymentStatus = PaymentStatus.Failed;
            await ordersService.UpdateAsync(order); 
            return  Success<string>("PaymentMarkedAsFailed");
        }
    }
}
