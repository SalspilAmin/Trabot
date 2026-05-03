using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Education.Queries.Models;
using Tradify.Core.Features.Education.Queries.Results;
using Tradify.Core.Features.Store.Queries.Models;
using Tradify.Core.Features.Store.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Service.AbstractsServices;
using Microsoft.EntityFrameworkCore;
using Tradify.Core.Features.InstructorService.Queries.Results;
using Tradify.Core.Features.InstructorService.Queries.Models;


namespace Tradify.Core.Features.InstructorService.Queries.Handlers
{
    public class InstructorServiceQueryHandler : ResponseHandler
                                                     , IRequestHandler<GetInstructorServiceQuery, List<GetInstructorServiceResponse>>


    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly IInstructorsService instructorsService;
        private readonly IServiceService serviceService;
        #endregion

        #region Constructor
        public InstructorServiceQueryHandler(LocalizationService localization, IMapper mapper, IInstructorsService instructorsService
            , IServiceService serviceService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.instructorsService = instructorsService;
            this.serviceService = serviceService;
        }

        #endregion

        #region Mehtods
        

        // Get All Store List

        public async Task<List<GetInstructorServiceResponse>> Handle(GetInstructorServiceQuery request, CancellationToken cancellationToken)
        {


            var services = await serviceService.GetTableNoTracking()
                .Where(e=>e.InstructorId==request.Id).ToListAsync();

           

            var result = mapper.Map<List<GetInstructorServiceResponse>>(services);

            return result;
        }


        #endregion
    }


}
