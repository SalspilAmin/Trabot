using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Dashpoard.ProductSeller.Queries.Models;
using Tradify.Core.Features.Dashpoard.ProductSeller.Queries.Results;
using Tradify.Core.Features.Education.Queries.Models;
using Tradify.Core.Features.Education.Queries.Results;
using Tradify.Core.Features.Instructor.Queries.Results;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Features.Recomndition.Queries.Models;
using Tradify.Core.Features.Recomndition.Queries.Results;
using Tradify.Core.Features.Review.Queries.Models;
using Tradify.Core.Features.Review.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Data.Entities.Appointments;
using Tradify.Data.Enums;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;
using Twilio.TwiML.Voice;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Core.Features.Recomndition.Queries.Handler
{
    public class RecommendedQueryHandler : ResponseHandler
        , IRequestHandler<GetRecommendedInstructorsQuery, List<GetRecommendedInstructorsResponse>>
        , IRequestHandler<GetTopRatedProductsRecommendQuery, PaginatedResult<GetSellerProductPaginationReponse>>
        , IRequestHandler<GetTopRatedInstructorsQuery, PaginatedResult<GetRecommendedInstructorsResponse>>

    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly ICurrentUserService currentUserService;
        private readonly IInstructorsService instructorsService;
        private readonly IBookingsService bookingsService;
        private readonly IProductService productService;
        #endregion

        #region Constructor
        public RecommendedQueryHandler(LocalizationService localization, IMapper mapper
            , IInstructorsService instructorsService, IBookingsService bookingsService
            , ICurrentUserService currentUserService, IProductService productService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.instructorsService = instructorsService;
            this.bookingsService = bookingsService;
            this.currentUserService = currentUserService;
            this.productService = productService;
        }

        #endregion

        #region Mehtods

        private async Task<List<GetRecommendedInstructorsResponse>>
GetPopularInstructors(CancellationToken cancellationToken)
        {
            var popularInstructorIds = await bookingsService.GetTableNoTracking()
                .Where(b => b.InstructorId != null)
                .GroupBy(b => b.InstructorId!.Value)
                .OrderByDescending(g => g.Count())
                .Take(10)
                .Select(g => g.Key)
                .ToListAsync(cancellationToken);

            var instructors = await instructorsService.GetTableNoTracking()
                .Where(i =>
                    i.IsActive &&
                    popularInstructorIds.Contains(i.Id))
                .ToListAsync(cancellationToken);

            return mapper.Map<List<GetRecommendedInstructorsResponse>>(instructors);
        }

        public async Task<List<GetRecommendedInstructorsResponse>> Handle(GetRecommendedInstructorsQuery request, CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();

            // الإنستركتورز اللي المستخدم حجز معاهم

            var bookedInstructorIds = await bookingsService.GetTableNoTracking()
                .Include(b => b.Customer)
        .Where(b =>
            b.CustomerId == userId &&
            b.InstructorId != null)
        .Select(b => b.InstructorId!.Value)
        .Distinct().ToListAsync();

            if (!bookedInstructorIds.Any())
                return await GetPopularInstructors(cancellationToken);


            // Find Similar Users
            var similarUsers = await bookingsService.GetTableNoTracking()
                  .Where(b =>
                      b.CustomerId != userId &&
                      b.InstructorId != null &&
                      bookedInstructorIds.Contains(b.InstructorId.Value))
                  .GroupBy(b => b.CustomerId)
                  .Select(g => new
                  {
                      UserId = g.Key,
                      SimilarityScore = g.Count()
                  })
                  .OrderByDescending(x => x.SimilarityScore)
                  .Take(20)
                  .ToListAsync();

            var similarUserIds = similarUsers
                .Select(x => x.UserId)
                .ToList();



            if (!similarUserIds.Any())
                return await GetPopularInstructors(cancellationToken);



            // Recommended Instructors

            var recommendedInstructorIds = await bookingsService.GetTableNoTracking()
                .Where(b =>
                    similarUserIds.Contains(b.CustomerId) &&
                    b.InstructorId != null &&
                    !bookedInstructorIds.Contains(b.InstructorId.Value))
                .GroupBy(b => b.InstructorId!.Value)
                .OrderByDescending(g => g.Count())

                .Select(g => g.Key)
                .ToListAsync(cancellationToken);

            if (!recommendedInstructorIds.Any())
                return await GetPopularInstructors(cancellationToken);

            if (recommendedInstructorIds.Count < 10)
            {
                var remainingIds = await instructorsService.GetTableNoTracking()
                    .Where(i =>
                        i.IsActive &&
                        !recommendedInstructorIds.Contains(i.Id) &&
                        !bookedInstructorIds.Contains(i.Id))
                    .Select(i => i.Id)
                    .ToListAsync(cancellationToken);

                recommendedInstructorIds.AddRange(
                    remainingIds.Take(10 - recommendedInstructorIds.Count));
            }

            recommendedInstructorIds = recommendedInstructorIds
    .Take(10)
    .ToList();



            var recommendInstructor = await instructorsService.GetTableNoTracking()
     .Where(i =>
         i.IsActive &&
         recommendedInstructorIds.Contains(i.Id))
     .ToListAsync(cancellationToken);



            recommendInstructor = recommendInstructor
                .OrderBy(i => recommendedInstructorIds.IndexOf(i.Id))
                .ToList();

            var result2 = mapper.Map<List<GetRecommendedInstructorsResponse>>(recommendInstructor);

            return result2;
        }



        public async Task<PaginatedResult<GetSellerProductPaginationReponse>> Handle( GetTopRatedProductsRecommendQuery request,CancellationToken cancellationToken)
        {


            var products = productService
     .GetTableNoTracking()
     .Where(x => x.Reviews.Any())
     .OrderByDescending(x => x.Reviews.Average(r => (double)r.Rating))
     .ThenByDescending(x => x.Reviews.Count());

            var result = await mapper
                .ProjectTo<GetSellerProductPaginationReponse>(products)
                .ToPaginationlist(request.PageNumber, request.PageSize);



            return result;
        }


        public async Task<PaginatedResult<GetRecommendedInstructorsResponse >> Handle(GetTopRatedInstructorsQuery request, CancellationToken cancellationToken)
        {


            var instructors = instructorsService
     .GetTableNoTracking()
     .Where(x => x.Reviews.Any())
     .OrderByDescending(x => x.Reviews.Average(r => (double)r.Rating))
     .ThenByDescending(x => x.Reviews.Count());

            var result = await mapper
                .ProjectTo<GetRecommendedInstructorsResponse>(instructors)
                .ToPaginationlist(request.PageNumber, request.PageSize);



            return result;
        }

        #endregion
    }
}