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


namespace Tradify.Core.Features.Education.Queries.Handlers
{
    public class EducationQueryHandler : ResponseHandler
                                                     , IRequestHandler<GetEducationByInstructorQuery, List<GetEducationByInstructorResponse>>


    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly IInstructorsService instructorsService;
        private readonly IEducationService educationService;
        #endregion

        #region Constructor
        public EducationQueryHandler(LocalizationService localization, IMapper mapper, IInstructorsService instructorsService
            ,IEducationService educationService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.instructorsService = instructorsService;
            this.educationService = educationService;
        }

        #endregion

        #region Mehtods
        

        // Get All Store List

        public async Task<List<GetEducationByInstructorResponse>> Handle(GetEducationByInstructorQuery request, CancellationToken cancellationToken)
        {


            var educations = await educationService.GetTableNoTracking()
                .Where(e=>e.InstructorId==request.Id).ToListAsync();

           

            var result = mapper.Map<List<GetEducationByInstructorResponse>>(educations);

            return result;
        }


        #endregion
    }


}
