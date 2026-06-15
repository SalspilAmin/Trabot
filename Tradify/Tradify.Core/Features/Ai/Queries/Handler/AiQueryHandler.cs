using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Ai.Queries.Models;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Features.Recomndition.Queries.Models;
using Tradify.Core.Features.Recomndition.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Data.Entities.Appointments;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;
using Tradify.Service.Services.Ai;
using Tradify.Service.Services.IdentityServices;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Tradify.Core.Features.Ai.Queries.Handler
{
    public class AiQueryHandler : ResponseHandler
        , IRequestHandler<AiQuery, PaginatedResult<GetSellerProductPaginationReponse>>
    {

        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly ICurrentUserService currentUserService;
        private readonly IAiService aiService;
        private readonly IProductService productService;
        #endregion

        #region Constructor
        public AiQueryHandler(LocalizationService localization, IMapper mapper
            , IInstructorsService instructorsService, IBookingsService bookingsService
            , ICurrentUserService currentUserService, IProductService productService
            ,IAiService aiService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.currentUserService = currentUserService;
            this.aiService = aiService;
            this.productService= productService;    
        }

        #endregion

        #region Mehtods
        private static readonly string[] Genders =
{
    "male",
    "female"
};

        private static string GenerateRandomBirthDate()
        {
            var start = new DateTime(1980, 1, 1);
            var end = new DateTime(2010, 12, 31);

            var range = (end - start).Days;

            return start
                .AddDays(Random.Shared.Next(range))
                .ToString("yyyy-MM-dd");
        }

        public async Task<PaginatedResult<GetSellerProductPaginationReponse>> Handle(
    AiQuery request,
    CancellationToken cancellationToken)
        {
            var airequest = new AiRequest
            {
                Birthdate = GenerateRandomBirthDate(),
                Gender = Genders[Random.Shared.Next(Genders.Length)],
                City = "Cairo",
                Country = "Egypt",
                Top_K = 15
            };

            var productIds =
                await aiService
                    .GetRecommendationsAsync(
                        airequest);

            var products =
                 productService
                    .GetTableNoTracking()
                    .Where(x => productIds.Contains(x.Id))
                    .AsQueryable();

            var result = await mapper
                .ProjectTo<GetSellerProductPaginationReponse>(products)
                .ToPaginationlist(request.PageNumber, request.PageSize);



            return result;

           
        }
        #endregion
    }
}
