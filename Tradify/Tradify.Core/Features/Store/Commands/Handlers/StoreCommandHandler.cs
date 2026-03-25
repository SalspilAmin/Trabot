using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.Store.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Infrastructure.Context;
using Tradify.Service.AbstractsServices;

namespace Tradify.Core.Features.Store.Commands.Handlers
{
    public class StoreCommandHandler : ResponseHandler,
                                          IRequestHandler<AddStoreCommand, Response<int>>,
                                          IRequestHandler<UpdateStoreCommand, Response<string>>,
                                          IRequestHandler<ActivateStoreCommand, Response<string>>,
                                          IRequestHandler<DeactivateStoreCommand, Response<string>>,
                                          IRequestHandler<DeleteStoreCommand, Response<string>>,
                                          IRequestHandler<RestoreStoreCommand, Response<string>>



    {
        #region Fields
        private readonly LocalizationService localize;
        
        private readonly IStoreService storeService;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext applicationDbContext;

        #endregion

        #region Constructor
        public StoreCommandHandler(IStoreService storeService, 
            ApplicationDbContext applicationDbContext,
                                     IMapper mapper,
                                     LocalizationService localize) : base(localize)
        {
            this.storeService = storeService;
            this.mapper = mapper;
            this.localize = localize;
            this.applicationDbContext = applicationDbContext;
        }
        #endregion

        #region Methods

        
        public async Task<Response<int>> Handle(AddStoreCommand request, CancellationToken cancellationToken)
        {
            var store = mapper.Map<Stores>(request);
            var existingStore = await applicationDbContext.Stores
                .FirstOrDefaultAsync(s => s.SellerId == request.SellerId, cancellationToken);

            if (existingStore != null)
            {
                return BadRequest<int>(localize.Get("SellerHasStore"));
            }
            var result = await storeService.AddAsync(store);
            if (result == null) return BadRequest<int>(localize.Get("FailedToAddStore"));
            await storeService.SaveChangesAsync();
            return Success<int>(store.Id, localize.Get("StoreAddedSuccessfully"));
        }


      
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
