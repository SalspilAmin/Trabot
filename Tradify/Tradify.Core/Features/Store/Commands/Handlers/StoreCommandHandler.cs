using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.Store.Commands.Models;
using Tradify.Core.Features.User.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Infrastructure.Context;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.Store.Commands.Handlers
{
    public class StoreCommandHandler : ResponseHandler,
                                          IRequestHandler<AddStoreCommand, Response<string>>,
                                          IRequestHandler<UpdateStoreCommand, Response<string>>,
                                          IRequestHandler<ActivateStoreCommand, Response<string>>,
                                          IRequestHandler<DeactivateStoreCommand, Response<string>>,
                                          IRequestHandler<DeleteStoreCommand, Response<string>>,
                                          IRequestHandler<RestoreStoreCommand, Response<string>>,
                                          IRequestHandler<AddStoreWithImageCommand, Response<string>>





    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly UserManager<Tradify.Data.Entities.Identity.User> _userManager;

        private readonly IStoreService storeService;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly ISellerService sellerService;
        private readonly IFileService fileService;  
        private readonly IStoreImageService storeImageService;  

        #endregion

        #region Constructor
        public StoreCommandHandler(IStoreService storeService 
                                     ,ApplicationDbContext applicationDbContext
                                     ,IMapper mapper
                                     , UserManager<Tradify.Data.Entities.Identity.User> userManager
                                     , LocalizationService localize
                                     , ISellerService sellerService
                                     , IFileService fileService
                                     , IStoreImageService storeImageService) : base(localize)
        {
            this.storeService = storeService;
            this._userManager = userManager;

            this.mapper = mapper;
            this.localize = localize;
            this.applicationDbContext = applicationDbContext;
            this.sellerService = sellerService;
            this.fileService = fileService;
            this.storeImageService = storeImageService;
        }
        #endregion

        #region Methods

        // Add Store
        public async Task<Response<string>> Handle(AddStoreCommand request, CancellationToken cancellationToken)
        {
            var store = mapper.Map<Stores>(request);


            var result = await storeService.AddStoreAsync(store);
            switch (result.Item1)
            {

                case "SellerNotFound":
                    return BadRequest<string>(localize.Get("SellerNotFound"));
                    break;
                case "SellerNotActive":
                    return BadRequest<string>(localize.Get("SellerNotActive"));
                    break;

                case "UserNotFound":
                    return BadRequest<string>(localize.Get("UserNotFound"));
                    break;

                    
                case "SellerConectWithDeletedUser":
                    return BadRequest<string>(localize.Get("SellerConectWithDeletedUser"));
                    break;
                case "SellerAlreadyHasStore":
                    return BadRequest<string>(localize.Get("SellerAlreadyHasStore"));

                    break;

                case "StoreNameAlreadyExists":
                    return BadRequest<string>(localize.Get("StoreNameAlreadyExists"));

                    break;

                case "Failed":
                    return BadRequest<string>(localize.Get("Failed"));
                    break;
                case "Success":
                    return Success<string>(result.Item1, meta: result.Item2);
                    break;
                default: return BadRequest<string>(result.Item1);
            }
            

        }


        // Add Store With Image


        public async Task<Response<string>> Handle(AddStoreWithImageCommand request, CancellationToken cancellationToken)
        {
         
            var store = mapper.Map<Stores>(request);

            var result = await storeService.AddStoreAsync(store);

            if (result.Item1 != "Success")
            {
                switch (result.Item1)
                {
                    case "SellerNotFound":
                        return BadRequest<string>(localize.Get("SellerNotFound"));
                    case "SellerNotActive":
                        return BadRequest<string>(localize.Get("SellerNotActive"));
                    case "UserNotFound":
                        return BadRequest<string>(localize.Get("UserNotFound"));
                    case "SellerConectWithDeletedUser":
                        return BadRequest<string>(localize.Get("SellerConectWithDeletedUser"));
                    case "SellerAlreadyHasStore":
                        return BadRequest<string>(localize.Get("SellerAlreadyHasStore"));
                    case "StoreNameAlreadyExists":
                        return BadRequest<string>(localize.Get("StoreNameAlreadyExists"));
                    default:
                        return BadRequest<string>(localize.Get("Failed"));
                }
            }

            var storeId = result.Item2.Value;

            // ✅ رفع الصورة
            var imagePath = await fileService.UploadGenericAsync(
                UploadFolder.Store,
                storeId,
                request.Image);

            if (!imagePath.StartsWith("/"))
            {

                return BadRequest<string>(localize.Get(imagePath));
            }

            // ✅ حفظ الصورة
            var storeImage = new Data.Entities.StoreImage
            {
                StoreId = storeId,
                MediaPath = imagePath
            };

            await storeImageService.AddAsync(storeImage);
            await storeImageService.SaveChangesAsync();

            return Success<string>("Success", meta: storeId);
        }




        // Update Store
        public async Task<Response<string>> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
        {
            var store = await storeService.GetByIdAsync(request.Id);
            if (store == null) return NotFound<string>(localize.Get("StoreNotFound"));
           
            if (store.SellerId != request.SellerId)
                return  BadRequest<string>(localize.Get("NotOwner"));

            mapper.Map(request,  store);
            await storeService.UpdateAsync(store);
            await storeService.SaveChangesAsync();

            return Success<string>(localize.Get("StoreUpdatedSuccessfully"));
        }


            

        public async Task<Response<string>> Handle(ActivateStoreCommand request, CancellationToken cancellationToken)
        {
            var store = await storeService.GetTableAsTracking()
                .FirstOrDefaultAsync(s => s.SellerId == request.Id, cancellationToken);

            if (store == null)
                return NotFound<string>(localize.Get("StoreNotFound"));

            if (store.IsDeleted)
                return BadRequest<string>(localize.Get("StoreIsDeleted"));

            if (store.IsActive)
                return BadRequest<string>(localize.Get("StoreAlreadyActivated"));

            store.IsActive = true;
            await storeService.UpdateAsync(store);
            await storeService.SaveChangesAsync();

            return Success<string>(localize.Get("StoreActivatedSuccessfully"));
        }

        public async Task<Response<string>> Handle(DeactivateStoreCommand request, CancellationToken cancellationToken)
        {
            var store = await storeService.GetTableAsTracking()
                .FirstOrDefaultAsync(s => s.SellerId == request.Id, cancellationToken);

            if (store == null)
                return NotFound<string>(localize.Get("StoreNotFound"));

            if (store.IsDeleted)
                return BadRequest<string>(localize.Get("StoreIsDeleted"));

            if (!store.IsActive)
                return BadRequest<string>(localize.Get("StoreAlreadyNotActive"));

            store.IsActive = false;
            await storeService.UpdateAsync(store);
            await storeService.SaveChangesAsync();

            return Success<string>(localize.Get("StoreDeactivateSuccessfully"));
        }

        public async Task<Response<string>> Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
        {
            var store = await storeService.GetByIdAsync(request.Id);

            if (store == null)
                return BadRequest<string>(localize.Get("StoreNotFound"));
            

            if (store.IsDeleted)
                return BadRequest<string>(localize.Get("StoreAlreadyDeleted"));
           

            store.IsDeleted = true;

            await storeService.UpdateAsync(store);
            await storeService.SaveChangesAsync();
            return Success<string>(localize.Get("StoreDeletedSuccessfully"));

        }

        public async Task<Response<string>> Handle(RestoreStoreCommand request, CancellationToken cancellationToken)
        {
            var store = await storeService.GetTableIgnoreQueryFilters()
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (store == null)
                return BadRequest<string>(localize.Get("StoreNotFound"));


            if (!store.IsDeleted)
                return BadRequest<string>(localize.Get("StoreIsNotDeleted"));

            

            store.IsDeleted = false;

            await storeService.UpdateAsync(store);
            await storeService.SaveChangesAsync();
            return Success<string>(localize.Get("StoreRestoredSuccessfully"));

        }

        #endregion

    }
}
