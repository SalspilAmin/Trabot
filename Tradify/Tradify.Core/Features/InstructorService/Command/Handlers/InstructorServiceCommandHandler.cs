using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Certification.Command.Models;
using Tradify.Core.Features.Education.Command.Models;
using Tradify.Core.Features.InstructorService.Command.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities.Appointments;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.AuthorizationServices;
using Tradify.Service.Services;
using Microsoft.EntityFrameworkCore;
namespace Tradify.Core.Features.InstructorService.Command.Handlers
{
    public class InstructorServiceCommandHandler : ResponseHandler,
                                         IRequestHandler<AddInstructorServiceCommand, Response<string>>,
                                         IRequestHandler<UpdateInstructorServiceCommand, Response<string>>,
                                         IRequestHandler<DeleteInstructorServiceCommand, Response<string>>
        




    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly IInstructorsService instructorsService;
        private readonly IMapper mapper;
        private readonly IServiceService serviceService;
        private readonly ICurrentUserService currentUserService;

        #endregion

        #region Constructor
        public InstructorServiceCommandHandler(IMapper mapper,
                                     IInstructorsService instructorsService,
                                     LocalizationService localize,
                                     IServiceService serviceService,
                                     ICurrentUserService currentUserService ) : base(localize)
        {
            this.mapper = mapper;
            this.localize = localize;
            this.instructorsService = instructorsService;
            this.serviceService = serviceService;
            this.currentUserService = currentUserService;
        }
        #endregion

        #region Methods

        // Add Service

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



        //Update Instructor Service 

        public async Task<Response<string>> Handle(UpdateInstructorServiceCommand request, CancellationToken cancellationToken)
        {
            var curantUser = currentUserService.GetUserId();
            var instructor = await instructorsService.GetTableNoTracking()
                .FirstOrDefaultAsync(i => i.UserId == curantUser);

            if (instructor == null)
                return BadRequest<string>(localize.Get("InstructorNotFound"));

            var service = await serviceService.GetTableAsTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id && c.InstructorId == instructor.Id);

            if (service == null)
                return BadRequest<string>(localize.Get("ServiceNotFound"));

            service.Name = request.Name;


            await serviceService.SaveChangesAsync();

            return Success<string>(localize.Get("ServiceUpdatedSuccessfully"));
        }


        // Delete Instructor Service 


        public async Task<Response<string>> Handle(DeleteInstructorServiceCommand request, CancellationToken cancellationToken)
        {
            var curantUser = currentUserService.GetUserId();
            var instructor = await instructorsService.GetTableNoTracking()
                .FirstOrDefaultAsync(i => i.UserId == curantUser);

            if (instructor == null)
                return BadRequest<string>(localize.Get("InstructorNotFound"));

            var service = await serviceService.GetTableAsTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id && c.InstructorId == instructor.Id);

            if (service == null)
                return BadRequest<string>(localize.Get("ServiceNotFound"));


            await serviceService.DeleteAsync(service);
            await serviceService.SaveChangesAsync();
            return Success<string>(localize.Get("ServiceDeletedSuccessfully"));

        }





        #endregion
    }
}
