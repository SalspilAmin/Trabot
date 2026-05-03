using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Education.Command.Models;
using Tradify.Core.Features.InstructorSchedules.Command.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities.Appointments;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.AuthorizationServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.InstructorSchedules.Command.Handlers
{
    public class InstructorSchedulesCommandHandler : ResponseHandler,
                                         IRequestHandler<AddInstructorSchedulesCommand, Response<string>>




    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly IInstructorsService instructorsService;
        private readonly IMapper mapper;
        private readonly IInstructorSchedulesService instructorSchedulesService;

        #endregion

        #region Constructor
        public InstructorSchedulesCommandHandler(IMapper mapper,
                                     IInstructorsService instructorsService,
                                     LocalizationService localize,
                                    IInstructorSchedulesService instructorSchedulesService) : base(localize)
        {
            this.mapper = mapper;
            this.localize = localize;
            this.instructorsService = instructorsService;
            this.instructorSchedulesService = instructorSchedulesService;
        }
        #endregion

        #region Methods

        // Add education

        public async Task<Response<string>> Handle(AddInstructorSchedulesCommand request, CancellationToken cancellationToken)
        {
            var schedules = mapper.Map<Data.Entities.Appointments.InstructorSchedules>(request);


            var result = await instructorSchedulesService.AddInstructorSchedulesAsync(schedules);

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
