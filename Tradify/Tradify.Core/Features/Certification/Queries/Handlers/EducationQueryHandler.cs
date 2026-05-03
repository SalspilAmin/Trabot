using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Certification.Queries.Models;
using Tradify.Core.Features.Certification.Queries.Results;
using Tradify.Core.Features.Education.Queries.Models;
using Tradify.Core.Features.Education.Queries.Results;
using Tradify.Core.Features.Store.Queries.Models;
using Tradify.Core.Features.Store.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Service.AbstractsServices;


namespace Tradify.Core.Features.Certification.Queries.Handlers
{
    public class EducationQueryHandler : ResponseHandler
                                                     , IRequestHandler<GetCertificationByInstructorQuery, List<GetCertificationByInstructorResponse>>


    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly IInstructorsService instructorsService;
        private readonly ICertificationsService certificationsService;
        #endregion

        #region Constructor
        public EducationQueryHandler(LocalizationService localization, IMapper mapper, IInstructorsService instructorsService
            , ICertificationsService certificationsService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.instructorsService = instructorsService;
            this.certificationsService = certificationsService;
        }

        #endregion

        #region Mehtods
        

        // Get All Store List

        public async Task<List<GetCertificationByInstructorResponse>> Handle(GetCertificationByInstructorQuery request, CancellationToken cancellationToken)
        {


            var certification = await certificationsService.GetTableNoTracking()
                .Where(e=>e.InstructorId==request.Id).ToListAsync();

           

            var result = mapper.Map<List<GetCertificationByInstructorResponse>>(certification);

            return result;
        }


        #endregion
    }


}
