using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Categorie.Queries.Models;
using Tradify.Core.Features.Categorie.Queries.Results;
using Tradify.Core.Features.Instructor.Queries.Models;
using Tradify.Core.Features.Instructor.Queries.Results;
using Tradify.Core.Features.Product.Queries.Models;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Data.Entities;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;
using Twilio.TwiML.Messaging;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Core.Features.Instructor.Queries.Handlers
{
    public class InstructorQueryHandler : ResponseHandler, IRequestHandler<GetInstructorJopTitleQuery, Response<PaginatedResult<GetInstructorJopTitleResponse>>>
                                                         , IRequestHandler<GetInstructorByIdQuery, Response<GetInstructorByIdResponse>>
                                                         , IRequestHandler<GetInstructorPagnitionQuery, Response<PaginatedResult<GetInstructorPagnitionRespons>>>
                                                         , IRequestHandler<GetInstructorWithDiscountQuery, PaginatedResult<GetInstructorWithDiscountResponse>>




    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly ICurrentUserService currentUserService;
        private readonly IInstructorImageService instructorImageService;
        private readonly IInstructorsService instructorsService;
        private readonly IStoreService storeService;




        #endregion

        #region Constructor

        public InstructorQueryHandler(LocalizationService localization,
             IMapper mapper, ICurrentUserService currentUserService
            , IInstructorsService instructorsService
            , IInstructorImageService instructorImageService
            ,IStoreService storeService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.currentUserService = currentUserService;
            this.instructorsService = instructorsService;
            this.instructorImageService = instructorImageService;
            this.storeService = storeService;   

        }

        #endregion

        #region Mehtods

        //Get Instructor Jop Title By Store Id 

        #region Get Instructor Jop Title

        public async Task<Response<PaginatedResult<GetInstructorJopTitleResponse>>> Handle(GetInstructorJopTitleQuery request, CancellationToken cancellationToken)
        {

            var query = instructorsService
                      .GetTableNoTracking();



            // Store
            if (request.StoreId.HasValue)
            {
                var store = await storeService.GetTableNoTracking()
                                          .Where(s => s.Id == request.StoreId)
                                          .Select(s => new { s.Id, s.Type })
                                          .FirstOrDefaultAsync();

                if (store == null)
                    return BadRequest<PaginatedResult<GetInstructorJopTitleResponse>>(localization.Get("StoreNotFound"));

                if (store.Type != Data.Enums.StoreType.Service)
                    return BadRequest<PaginatedResult<GetInstructorJopTitleResponse>>(localization.Get("ThisStoreTypeDosn'tSupportInstructors"));

                query = query.Where(i => i.StoreId == store.Id);
            }






                      var result = await query
                                               .Where(x => !string.IsNullOrEmpty(x.JobTitle))
                                               .Select(x => x.JobTitle)
                                               .Distinct()
                                               .OrderBy(x => x)
                                               .Select(x => new GetInstructorJopTitleResponse
                                               {
                                                         JobTitle = x
                                               })
                                               .ToPaginationlist(request.PageNumber, request.PageSize);

            return Success( result);
        }
        #endregion

        //Get Instructor By Id

        #region Get Instructor By Id
        public async Task<Response<GetInstructorByIdResponse>> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
        {
            var today = DateTime.UtcNow.DayOfWeek;


            var instructor = await instructorsService
                .GetTableNoTracking().Include(p => p.Schedules)

                .FirstOrDefaultAsync(p => p.Id == request.Id);




            if (instructor == null) return NotFound<GetInstructorByIdResponse>(localization.Get("InstructorNotFound"));

            var result = mapper.Map<GetInstructorByIdResponse>(instructor);

           
            result.AvailableToday = instructor.Schedules
                  .Any(s => s.IsAvailable &&
                        s.Day == today && (s.Capacity - s.ReservedCount) > 0);

         
            return Success<GetInstructorByIdResponse>(result);

        }
        #endregion

        //Get Instructor Pagntion

        #region Get Instructor Pangtion

        public async Task<Response<PaginatedResult<GetInstructorPagnitionRespons>>> Handle(GetInstructorPagnitionQuery request, CancellationToken cancellationToken)
        {
            var today = DateTime.UtcNow.DayOfWeek;


            var instructors = instructorsService
                .GetTableNoTracking().Include(p => p.Schedules).AsQueryable();

            // Store
            if (request.StoreId.HasValue)
            {
                var store = await storeService
                                   .GetTableNoTracking()
                                   .Where(s => s.Id == request.StoreId)
                                   .Select(s => new { s.Id, s.Type })
                                   .FirstOrDefaultAsync();
                if (store == null)
                    return BadRequest<PaginatedResult<GetInstructorPagnitionRespons>>(localization.Get("StoreNotFound"));

                if (store.Type != Data.Enums.StoreType.Service)
                    return BadRequest<PaginatedResult<GetInstructorPagnitionRespons>>(localization.Get("ThisStoreTypeDosn'tSupportInstructors"));


                instructors = instructors.Where(p =>
                    p.StoreId == request.StoreId);
            }


            // Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();
                instructors = instructors.Where(p =>
                              EF.Functions.Like(p.Name, $"%{search}%"));

            }

            // JopTitle
            if (!string.IsNullOrWhiteSpace(request.JobTitle))
            {
                var jobTitle = request.JobTitle.Trim();
                instructors = instructors.Where(p =>
                              EF.Functions.Like(p.JobTitle, $"%{jobTitle}%"));

            }


            //  Min Price
            if (request.MinPrice.HasValue)
            {
                instructors = instructors.Where(i =>i.PricePerSession >= request.MinPrice);
            }

            //  Max Price
            if (request.MaxPrice.HasValue)
            {
                instructors = instructors.Where(i => i.PricePerSession <= request.MaxPrice);
            }


            // rating
            if (request.MinRating.HasValue)
            {
                var minRating = request.MinRating.Value;

                instructors = instructors.Where(p =>
                    p.Reviews.Any() &&
                    p.Reviews.Average(r => (double)r.Rating) >= minRating);
            }

            //  Min Years Of Exprianc

            if (request.MinYearsOfExperience.HasValue)
            {
                instructors = instructors.Where(i => i.YearsOfExperience >= request.MinYearsOfExperience);
            }

            // AvalipalToday

            if (request.AvailableToday.HasValue)
            {
                var available = request.AvailableToday.Value;

                instructors = instructors.Where(i =>
                    i.Schedules.Any(s =>
                        s.IsAvailable &&
                        s.Day == today &&
                        (s.Capacity - s.ReservedCount) > 0) == available);
            }

           
            instructors = instructors.OrderByDescending(i => i.Id);



            var result = await mapper
                                  .ProjectTo<GetInstructorPagnitionRespons>(instructors)
                                  .ToPaginationlist(request.PageNumber, request.PageSize);

            var ids = result.Data.Select(x => x.Id).ToList();

            var availableIds = await instructorsService.GetTableNoTracking()
                .Where(i => ids.Contains(i.Id))
                .Select(i => new
                {
                    i.Id,
                    Available = i.Schedules.Any(s =>
                        s.IsAvailable &&
                        s.Day == today &&
                        (s.Capacity - s.ReservedCount) > 0)
                }).ToListAsync();

            foreach (var item in result.Data)
            {
                item.AvailableToday =
                    availableIds.First(x => x.Id == item.Id).Available;
            }


            return Success(result);

        }

        #endregion

        // Get Instructor With Discount
        #region Get Instructor With Discount
        public async Task<PaginatedResult<GetInstructorWithDiscountResponse>> Handle(GetInstructorWithDiscountQuery request, CancellationToken cancellationToken)
        {

            var instructors = instructorsService
                .GetTableNoTracking().Where(i=>i.Discount>0);

            instructors = instructors.OrderByDescending(i => i.Id);



            var result = await mapper
                                  .ProjectTo<GetInstructorWithDiscountResponse>(instructors)
                                  .ToPaginationlist(request.PageNumber, request.PageSize);

            return result;
        }

        #endregion

        #endregion
    }
}




