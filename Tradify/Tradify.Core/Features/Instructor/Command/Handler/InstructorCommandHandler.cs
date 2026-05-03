using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Instructor.Command.Models;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.Store.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Appointments;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.AuthorizationServices;
using Tradify.Service.Services;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Core.Features.Instructor.Command.Handler
{
    public class InstructorCommandHandler : ResponseHandler,
                                         IRequestHandler<AddInstructorCommand, Response<string>>,
                                         IRequestHandler<AddInstructorWithImageCommand, Response<string>>




    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly IStoreService storeService;
        private readonly IInstructorsService instructorsService;
        private readonly IMapper mapper;
        private readonly ICurrentUserService currentUserService;
        private readonly ISellerService sellerService;
        private readonly IFileService fileService;
        private readonly IAuthorizationService authorizationService;
        private readonly IInstructorImageService instructorImageService;

        #endregion

        #region Constructor
        public InstructorCommandHandler(IMapper mapper,
                                     IStoreService storeService,
                                     IInstructorsService instructorsService,
                                     ICurrentUserService currentUserService,
                                     IAuthorizationService authorizationService,
                                     LocalizationService localize
                                    , ISellerService sellerService
                                    , IFileService fileService
                                    , IInstructorImageService instructorImageService) : base(localize)
        {
            this.mapper = mapper;
            this.localize = localize;
            this.storeService = storeService;
            this.currentUserService = currentUserService;
            this.authorizationService = authorizationService;
            this.sellerService = sellerService;
            this.fileService = fileService;
            this.instructorsService = instructorsService;
            this.instructorImageService = instructorImageService;
        }
        #endregion

        #region Methods

        // Add Instructor

        public async Task<Response<string>> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = mapper.Map<Instructors>(request);


            var result = await instructorsService.AddInstructorAsync(instructor);

            if (result.Item1 != "Success")
            {
                return BadRequest<string>(localize.Get(result.Item1));
            }
            else
            {
                return Success<string>("Success", meta: result.Item2);
            }


        }


        // Add Instructor With Image
        public async Task<Response<string>> Handle(AddInstructorWithImageCommand request, CancellationToken cancellationToken)
        {
            var instructor = mapper.Map<Instructors>(request);

          
            var result = await instructorsService.AddInstructorAsync(instructor);

            if (result.Item1 != "Success")
            {
                return BadRequest<string>(localize.Get(result.Item1));
            }


            var instructorid = result.Item2.Value;


            // Upload Instructor Image 

            var folderName = $"{UploadFolder.Instructor}/{instructorid}";

            var uploadResult = await fileService.UploadImageAsync(
                         request.Image,
                         folderName);

            if (uploadResult.Error != "Success")
            {
                return BadRequest<string>(localize.Get(uploadResult.Error));
            }


            // Save 
            var instructorImage = new InstructorImage
            {
                InstructorId = instructorid,
                MediaPath = uploadResult.Url,
                PublicId = uploadResult.PublicId
            };


            await instructorImageService.AddAsync(instructorImage);
            await instructorImageService.SaveChangesAsync();

            return Success<string>("Success", meta: instructorid);


        }



        #endregion
    }
}