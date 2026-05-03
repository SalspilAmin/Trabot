using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Education.Command.Models;
using Tradify.Core.Features.InstructorService.Command.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities.Appointments;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.AuthorizationServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.InstructorService.Command.Handlers
{
    public class InstructorServiceCommandHandler : ResponseHandler,
                                         IRequestHandler<AddInstructorServiceCommand, Response<string>>




    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly IInstructorsService instructorsService;
        private readonly IMapper mapper;
        private readonly IServiceService serviceService;

        #endregion

        #region Constructor
        public InstructorServiceCommandHandler(IMapper mapper,
                                     IInstructorsService instructorsService,
                                     LocalizationService localize,
                                     IServiceService serviceService) : base(localize)
        {
            this.mapper = mapper;
            this.localize = localize;
            this.instructorsService = instructorsService;
            this.serviceService = serviceService;
        }
        #endregion

        #region Methods

        // Add education

        public async Task<Response<string>> Handle(AddInstructorServiceCommand request, CancellationToken cancellationToken)
        {
            var service = mapper.Map<Data.Entities.Appointments.Service>(request);


            var result = await serviceService.AddServiceAsync(service);

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
