using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Certification.Command.Models;
using Tradify.Core.Features.Education.Command.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities.Appointments;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.AuthorizationServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.Certification.Command.Handlers
{
    public class CertificationCommandHandler : ResponseHandler,
                                         IRequestHandler<AddInstructorCertificationCommand, Response<string>>




    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly IInstructorsService instructorsService;
        private readonly IMapper mapper;
        private readonly ICertificationsService certificationsService;

        #endregion

        #region Constructor
        public CertificationCommandHandler(IMapper mapper,
                                     IInstructorsService instructorsService,
                                     LocalizationService localize,
                                     ICertificationsService certificationsService) : base(localize)
        {
            this.mapper = mapper;
            this.localize = localize;
            this.instructorsService = instructorsService;
            this.certificationsService = certificationsService;
        }
        #endregion

        #region Methods

        // Add education

        public async Task<Response<string>> Handle(AddInstructorCertificationCommand request, CancellationToken cancellationToken)
        {
            var certification = mapper.Map<Data.Entities.Appointments.Certifications>(request);


            var result = await certificationsService.AddCertificationAsync(certification);

            if (result.Item1 != "Success")
            {
                return BadRequest<string>(localize.Get(result.Item1));
            }
            else
            {
                return Success<string>("Success", meta: result.Item2);
            }


        }


       



        #endregion
    }
}
