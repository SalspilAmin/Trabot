using AutoMapper;
using MediatR;
using Tradify.Core.Bases;
using Tradify.Core.Features.InstructorSchedules.Command.Models;
using Tradify.Core.Features.InstructorService.Command.Models;
using Tradify.Core.Resources.Service;
using Tradify.Service.AbstractsServices;
using Microsoft.EntityFrameworkCore;
namespace Tradify.Core.Features.InstructorSchedules.Command.Handlers
{
    public class InstructorSchedulesCommandHandler : ResponseHandler,
                                         IRequestHandler<AddInstructorSchedulesCommand, Response<string>>,
                                         IRequestHandler<UpdateInstructorSchedulesCommand, Response<string>>,
                                         IRequestHandler<DeleteInstructorSchedulesCommand, Response<string>>,
                                         IRequestHandler<RestoreInstructorSchedulesCommand, Response<string>>






    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly IInstructorsService instructorsService;
        private readonly IMapper mapper;
        private readonly IInstructorSchedulesService instructorSchedulesService;
        private readonly ICurrentUserService currentUserService;

        #endregion

        #region Constructor
        public InstructorSchedulesCommandHandler(IMapper mapper,
                                     IInstructorsService instructorsService,
                                     LocalizationService localize,
                                    IInstructorSchedulesService instructorSchedulesService,
                                    ICurrentUserService currentUserService  ) : base(localize)
        {
            this.mapper = mapper;
            this.localize = localize;
            this.instructorsService = instructorsService;
            this.instructorSchedulesService = instructorSchedulesService;
            this.currentUserService = currentUserService;
        }
        #endregion

        #region Methods

        // Add Schedules

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



        //Update Instructor Schedules 

        public async Task<Response<string>> Handle(UpdateInstructorSchedulesCommand request, CancellationToken cancellationToken)
        {
            var curantUser = currentUserService.GetUserId();
            var instructor = await instructorsService.GetTableNoTracking()
                .FirstOrDefaultAsync(i => i.UserId == curantUser);

            if (instructor == null)
                return BadRequest<string>(localize.Get("InstructorNotFound"));

            var service = await instructorSchedulesService.GetTableAsTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id && c.InstructorId == instructor.Id);

            if (service == null)
                return BadRequest<string>(localize.Get("SchedulesNotFound"));

            service.Capacity = request.Capacity;
            service.Day = request.Day;
            service.StartTime = request.StartTime;
            service.EndTime = request.EndTime;


            await instructorSchedulesService.SaveChangesAsync();

            return Success<string>(localize.Get("SchedulesUpdatedSuccessfully"));
        }


        //Delete Instructor Schedules 

        public async Task<Response<string>> Handle(DeleteInstructorSchedulesCommand request, CancellationToken cancellationToken)
        {
            var curantUser = currentUserService.GetUserId();
            var instructor = await instructorsService.GetTableNoTracking()
                .FirstOrDefaultAsync(i => i.UserId == curantUser);

            if (instructor == null)
                return BadRequest<string>(localize.Get("InstructorNotFound"));

            var service = await instructorSchedulesService.GetTableAsTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id && c.InstructorId == instructor.Id);

            if (service == null)
                return BadRequest<string>(localize.Get("SchedulesNotFound"));
            if (!service.IsAvailable)
                return BadRequest<string>(localize.Get("SchedulesIsAlradyNotAvilable"));

            service.IsAvailable = false;


            await instructorSchedulesService.SaveChangesAsync();

            return Success<string>(localize.Get("SchedulesDisAvailableSuccessfully"));
        }

        //Restore Instructor Schedules 

        public async Task<Response<string>> Handle(RestoreInstructorSchedulesCommand request, CancellationToken cancellationToken)
        {
            var curantUser = currentUserService.GetUserId();
            var instructor = await instructorsService.GetTableNoTracking()
                .FirstOrDefaultAsync(i => i.UserId == curantUser);

            if (instructor == null)
                return BadRequest<string>(localize.Get("InstructorNotFound"));

            var service = await instructorSchedulesService.GetTableAsTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id && c.InstructorId == instructor.Id);

            if (service == null)
                return BadRequest<string>(localize.Get("SchedulesNotFound"));
            if (service.IsAvailable)
                return BadRequest<string>(localize.Get("SchedulesIsAlradyAvilable"));

            service.IsAvailable = true;


            await instructorSchedulesService.SaveChangesAsync();

            return Success<string>(localize.Get("SchedulesAvailableSuccessfully"));
        }
        #endregion
    }
}
