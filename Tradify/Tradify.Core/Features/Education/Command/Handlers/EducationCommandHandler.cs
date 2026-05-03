using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Education.Command.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities.Appointments;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.AuthorizationServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.Education.Command.Handlers
{
    public class EducationCommandHandler : ResponseHandler,
                                         IRequestHandler<AddInstructorEducationCommand, Response<string>>




    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly IInstructorsService instructorsService;
        private readonly IMapper mapper;
        private readonly IEducationService educationService;

        #endregion

        #region Constructor
        public EducationCommandHandler(IMapper mapper,
                                     IInstructorsService instructorsService,
                                     LocalizationService localize,
                                     IEducationService educationService) : base(localize)
        {
            this.mapper = mapper;
            this.localize = localize;
            this.instructorsService = instructorsService;
            this.educationService = educationService;
        }
        #endregion

        #region Methods

        // Add education

        public async Task<Response<string>> Handle(AddInstructorEducationCommand request, CancellationToken cancellationToken)
        {
            var education = mapper.Map<Data.Entities.Appointments.Education>(request);


            var result = await educationService.AddEducationAsync(education);

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
