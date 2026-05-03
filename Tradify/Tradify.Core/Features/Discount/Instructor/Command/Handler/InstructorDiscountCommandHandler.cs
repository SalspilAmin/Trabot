using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Discount.Instructor.Command.Models;
using Tradify.Core.Resources.Service;
using Tradify.Service.AbstractsServices;

namespace Tradify.Core.Features.Discount.Instructor.Command.Handler 
{
    public class InstructorDiscountCommandHandler : ResponseHandler,
                                                       IRequestHandler<AddInstructorDiscountCommand, Response<string>>
                                                     , IRequestHandler<DeleteInstructorDiscountCommand, Response<string>>
                                                     , IRequestHandler<AddStoreServiceDiscountCommand, Response<string>>
                                                     , IRequestHandler<DeleteStoreServiceDiscountCommand, Response<string>>
    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly IStoreService storeService;
        private readonly IMapper mapper;
        private readonly ICurrentUserService currentUserService;
        private readonly ISellerService sellerService;
        private readonly IInstructorsService instructorsService;


        #endregion

        #region Constructor
        public InstructorDiscountCommandHandler(
                                     IMapper mapper,
                                     IStoreService storeService,
                                     ICurrentUserService currentUserService,
                                     LocalizationService localize,
                                     ISellerService sellerService,
                                     IInstructorsService instructorsService) : base(localize)
        {
            this.mapper = mapper;
            this.localize = localize;
            this.storeService = storeService;
            this.currentUserService = currentUserService;
            this.sellerService = sellerService;
            this.instructorsService = instructorsService;   
        }
        #endregion

        #region Methods


        // Add Instructor Discount 
        public async Task<Response<string>> Handle(AddInstructorDiscountCommand request, CancellationToken cancellationToken)
        {
            var userid =  currentUserService.GetUserId();

            var instructor = await instructorsService.GetTableAsTracking().FirstOrDefaultAsync(i=>i.UserId== userid);

           
            if (instructor == null)
                return NotFound<string>(localize.Get("instructorNotFound"));

            instructor.Discount = request.Discount;

            await instructorsService.UpdateAsync(instructor);
            await instructorsService.SaveChangesAsync();

            return Success<string>(localize.Get("DiscountAddedSuccessfully"));


        }

        // Delete Instructor Discount 


        public async Task<Response<string>> Handle(DeleteInstructorDiscountCommand request, CancellationToken cancellationToken)
        {
            var userid = currentUserService.GetUserId();

            var instructor = await instructorsService.GetTableAsTracking().FirstOrDefaultAsync(i => i.UserId == userid);


            if (instructor == null)
                return NotFound<string>(localize.Get("instructorNotFound"));

            instructor.Discount = 0;

            await instructorsService.UpdateAsync(instructor);
            await instructorsService.SaveChangesAsync();

            return Success<string>(localize.Get("DiscountDeletedSuccessfully"));
        }


        // Add Discount For Store That Have Service  
        public async Task<Response<string>> Handle(AddStoreServiceDiscountCommand request, CancellationToken cancellationToken)
        {
            var ValidSeller = await currentUserService.GetValidSellerContextAsync();

            if (ValidSeller.Error != null)
                return BadRequest<string>(localize.Get(ValidSeller.Error));

            var seller = ValidSeller.Seller;
            var store = ValidSeller.Store;

        
            var instructors = await instructorsService.GetTableAsTracking()
               .Where(v => v.StoreId == store.Id).ToListAsync();

            foreach (var instructor in instructors)
            {
                instructor.Discount = request.Discount;
            }

            await instructorsService.UpdateRangeAsync(instructors);
            await instructorsService.SaveChangesAsync();
            return Success<string>(localize.Get("DiscountAddedSuccessfully"));

        }




        public async Task<Response<string>> Handle(DeleteStoreServiceDiscountCommand request, CancellationToken cancellationToken)
        {
            var ValidSeller = await currentUserService.GetValidSellerContextAsync();

            if (ValidSeller.Error != null)
                return BadRequest<string>(localize.Get(ValidSeller.Error));

            var seller = ValidSeller.Seller;
            var store = ValidSeller.Store;


            var instructors = await instructorsService.GetTableAsTracking()
               .Where(v => v.StoreId == store.Id).ToListAsync();

            foreach (var instructor in instructors)
            {
                instructor.Discount = 0;
            }

            await instructorsService.UpdateRangeAsync(instructors);
            await instructorsService.SaveChangesAsync();

            return Success<string>(localize.Get("DiscountDeletedSuccessfully"));
        }


        #endregion


    }
}
