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
using Tradify.Core.Features.InstructorSchedules.Queries.Models;
using Tradify.Core.Features.InstructorSchedules.Queries.Results;


namespace Tradify.Core.Features.InstructorSchedules.Queries.Handlers
{
    public class InstructorSchedulesQueryHandler : ResponseHandler
                                                     , IRequestHandler<GetInstructorSchedulesQuery, List<GetInstructorSchedulesResponse>>


    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly IInstructorsService instructorsService;
        private readonly IInstructorSchedulesService instructorSchedulesService;
        #endregion

        #region Constructor
        public InstructorSchedulesQueryHandler(LocalizationService localization, IMapper mapper, IInstructorsService instructorsService
            , IInstructorSchedulesService instructorSchedulesService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.instructorsService = instructorsService;
            this.instructorSchedulesService = instructorSchedulesService;
        }

        #endregion

        #region Mehtods


        // Get All Instructor Schedules List

        public async Task<List<GetInstructorSchedulesResponse>> Handle(GetInstructorSchedulesQuery request, CancellationToken cancellationToken)
        {


            var schedules = await instructorSchedulesService.GetTableNoTracking()
                .Where(e=>e.InstructorId==request.Id).ToListAsync();

           

            var result = mapper.Map<List<GetInstructorSchedulesResponse>>(schedules);

            return result;
        }


        #endregion
    }


}
