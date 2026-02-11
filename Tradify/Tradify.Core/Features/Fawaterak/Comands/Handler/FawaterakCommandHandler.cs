using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Fawaterak.Comands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Data.Helpers.Fawaterak;
using Tradify.Data.Helpers.Fawaterak.Einvoice;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.FawaterakServices;

namespace Tradify.Core.Features.Fawaterak.Comands.Handler
{
    public class FawaterakCommandHandler : ResponseHandler,IRequestHandler<EInvoiceRequestLinkCommand,Response<EInvoiceResponseDataModel>>
    {
        private readonly LocalizationService localization;
        private readonly IFawaterakServices fawaterakServices;
        private readonly IOrdersService ordersService;
        private readonly IMapper mapper;
        public FawaterakCommandHandler(LocalizationService localization, IFawaterakServices fawaterak,IOrdersService ordersService,IMapper mapper) : base(localization)
        {
            this.localization = localization;
            this.fawaterakServices = fawaterak;
            this.ordersService = ordersService;
            this.mapper = mapper;       
        }

        public async Task<Response<EInvoiceResponseDataModel>> Handle(EInvoiceRequestLinkCommand request, CancellationToken cancellationToken)
        {
            var order = await ordersService.GetByIdAsync(request.orderId);
            // check of order
            var listOFCartProduct = order.cart.CartProducts.ToList();
            var listOfProducts = order.products.ToList();
            if (listOfProducts == null||listOFCartProduct==null) return BadRequest<EInvoiceResponseDataModel>("NotFound");
            foreach (var product in listOfProducts) 
            {

                var pro = mapper.Map<ProductINRequestApi>(product);
                var qunitity = listOFCartProduct.Where(x => x.ProductId == product.Id).Select(x => x.Quantity).FirstOrDefault();
                pro.quantity=qunitity;

                request.CartItems.Add(new CartItemModel() { Name = pro.name, Price = pro.price, Quantity = (int)pro.quantity });

            }

                var result = await fawaterakServices.CreateEInvoiceAsync(request);

            if (result != null)
            {
                return Success(result);
            }
            return BadRequest<EInvoiceResponseDataModel>(localization.Get("TryToRegisterAgain"));
        }

       
    }
}
