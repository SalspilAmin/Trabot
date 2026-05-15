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
using Microsoft.EntityFrameworkCore;

namespace Tradify.Core.Features.Certification.Command.Handlers
{
    public class CertificationCommandHandler : ResponseHandler,
                                         IRequestHandler<AddInstructorCertificationCommand, Response<string>>,
                                         IRequestHandler<UpdateInstructorCertificationCommand, Response<string>>,
                                         IRequestHandler<DeleteInstructorCertificationCommand, Response<string>>






    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly IInstructorsService instructorsService;
        private readonly IMapper mapper;
        private readonly ICertificationsService certificationsService;
        private readonly ICurrentUserService currentUserService;

        #endregion

        #region Constructor
        public CertificationCommandHandler(IMapper mapper,
                                     IInstructorsService instructorsService,
                                     LocalizationService localize,
                                     ICertificationsService certificationsService,
                                     ICurrentUserService currentUserService ) : base(localize)
        {
            this.mapper = mapper;
            this.localize = localize;
            this.instructorsService = instructorsService;
            this.certificationsService = certificationsService;
            this.currentUserService = currentUserService;
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


        //Update Instructor Certification 

        public async Task<Response<string>> Handle(UpdateInstructorCertificationCommand request, CancellationToken cancellationToken)
        {
            var curantUser = currentUserService.GetUserId();
            var instructor = await instructorsService.GetTableNoTracking()
                .FirstOrDefaultAsync(i => i.UserId == curantUser);

            if (instructor == null)
                return BadRequest<string>(localize.Get("InstructorNotFound"));

            var certification = await certificationsService.GetTableAsTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id && c.InstructorId == instructor.Id);

            if (certification == null)
                return BadRequest<string>(localize.Get("CertificationNotFound"));

            certification.Title = request.Title;
           

            await certificationsService.SaveChangesAsync();

            return Success<string>(localize.Get("CertificationUpdatedSuccessfully"));
        }


        // Delete Instructor Certification 


        public async Task<Response<string>> Handle(DeleteInstructorCertificationCommand request, CancellationToken cancellationToken)
        {
            var curantUser = currentUserService.GetUserId();
            var instructor = await instructorsService.GetTableNoTracking()
                .FirstOrDefaultAsync(i => i.UserId == curantUser);

            if (instructor == null)
                return BadRequest<string>(localize.Get("InstructorNotFound"));

            var certification = await certificationsService.GetTableAsTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id && c.InstructorId == instructor.Id);

            if (certification == null)
                return BadRequest<string>(localize.Get("CertificationNotFound"));


            await certificationsService.DeleteAsync(certification);
            await certificationsService.SaveChangesAsync();
            return Success<string>(localize.Get("CertificationDeletedSuccessfully"));

        }





        #endregion
    }
}
