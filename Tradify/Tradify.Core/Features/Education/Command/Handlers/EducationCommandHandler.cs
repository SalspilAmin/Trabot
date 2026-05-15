using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Education.Command.Models;
using Tradify.Core.Features.Store.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities.Appointments;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.AuthorizationServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.Education.Command.Handlers
{
    public class EducationCommandHandler : ResponseHandler,
                                         IRequestHandler<AddInstructorEducationCommand, Response<string>>,
                                         IRequestHandler<UpdateInstructorEducationCommand, Response<string>>,
                                         IRequestHandler<DeleteInstructorEducationCommand, Response<string>>






    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly IInstructorsService instructorsService;
        private readonly IMapper mapper;
        private readonly IEducationService educationService;
        private readonly ICurrentUserService currentUserService;

        #endregion

        #region Constructor
        public EducationCommandHandler(IMapper mapper,
                                     IInstructorsService instructorsService,
                                     LocalizationService localize,
                                     IEducationService educationService,
                                     ICurrentUserService currentUserService ) : base(localize)
        {
            this.mapper = mapper;
            this.localize = localize;
            this.instructorsService = instructorsService;
            this.educationService = educationService;
            this.currentUserService = currentUserService;
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


        //Update Instructor Education 

        public async Task<Response<string>> Handle(UpdateInstructorEducationCommand request, CancellationToken cancellationToken)
        {
            var curantUser =  currentUserService.GetUserId();
            var instructor = await instructorsService.GetTableNoTracking()
                .FirstOrDefaultAsync(i=>i.UserId==curantUser);

            if (instructor == null)
                return BadRequest<string>(localize.Get("InstructorNotFound"));

            var education = await educationService.GetTableAsTracking()
                .FirstOrDefaultAsync(e=>e.Id==request.Id && e.InstructorId==instructor.Id);

            if (education == null)
                return BadRequest<string>(localize.Get("EducationNotFound"));

            education.Degree = request.Degree;
            education.Institution = request.Institution;
            education.Year = request.Year;

            await educationService.SaveChangesAsync();

            return Success<string>(localize.Get("EducationUpdatedSuccessfully"));
        }


        // Delete Instructor Education 


        public async Task<Response<string>> Handle(DeleteInstructorEducationCommand request, CancellationToken cancellationToken)
        {
            var curantUser = currentUserService.GetUserId();
            var instructor = await instructorsService.GetTableNoTracking()
                .FirstOrDefaultAsync(i => i.UserId == curantUser);

            if (instructor == null)
                return BadRequest<string>(localize.Get("InstructorNotFound"));

            var education = await educationService.GetTableAsTracking()
                .FirstOrDefaultAsync(e => e.Id == request.Id && e.InstructorId == instructor.Id);

            if (education == null)
                return BadRequest<string>(localize.Get("EducationNotFound"));


            await educationService.DeleteAsync(education);
            await educationService.SaveChangesAsync();
            return Success<string>(localize.Get("EducationDeletedSuccessfully"));

        }


        #endregion
    }
}
