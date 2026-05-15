using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.InstructorImage.Command.Models;
using Tradify.Core.Features.StoreImage.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.InstructorImage.Command.Handlers
{
    public class InstructorImageCommandHandler : ResponseHandler
        , IRequestHandler<AddInstructorImageCommand, Response<string>>
        , IRequestHandler<UpdateInstructorImageCommand, Response<string>>
        , IRequestHandler<DeleteInstructorImageCommand, Response<string>>


    {

        #region Fields
        private readonly LocalizationService localize;
        private readonly IInstructorsService instructorsService;
        private readonly IFileService fileService;
        private readonly IInstructorImageService instructorImageService;
        private readonly IMapper mapper;
        private readonly ICurrentUserService currentUserService;
        private readonly ISellerService sellerService;


        #endregion

        #region Constructor
        public InstructorImageCommandHandler(IInstructorsService instructorsService,
                                     IMapper mapper,
                                     IInstructorImageService instructorImageService,
                                     IFileService fileService,
                                     ISellerService sellerService,
                                     ICurrentUserService currentUserService,
                                     LocalizationService localize) : base(localize)
        {
            this.instructorsService = instructorsService;
            this.mapper = mapper;
            this.fileService = fileService;
            this.instructorImageService = instructorImageService;
            this.localize = localize;
            this.currentUserService = currentUserService;
            this.sellerService = sellerService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddInstructorImageCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = currentUserService.GetUserId();

            var seller = await sellerService.GetTableNoTracking()
                 .FirstOrDefaultAsync(s => s.UserId == currentUserId);

            if (seller == null)
                return Unauthorized<string>("SellerNotFound");
            // 2️⃣ Get Instructor
            var instructor = await instructorsService.GetTableAsTracking()  
                     .FirstOrDefaultAsync(s => s.Id == request.InstructorId
                     &&s.Store.SellerId== seller.Id);




            if (instructor == null)
                return NotFound<string>(localize.Get("InstructorNotFound"));

            
            if (instructor.InstructorImage != null)
                return BadRequest<string>(localize.Get("InstructorHasImageAlrady"));


            var folderName = $"{UploadFolder.Instructor}/{request.InstructorId}";

            var uploadResult = await fileService.UploadImageAsync(
                         request.Image,
                         folderName);

            if (uploadResult.Error != "Success")
            {
                return BadRequest<string>(localize.Get(uploadResult.Error));
            }


            // 5️⃣ Save in DB
            var instructorimage = new Data.Entities.Appointments.InstructorImage
            {
                InstructorId = request.InstructorId,
                MediaPath = uploadResult.Url,
                PublicId = uploadResult.PublicId
            };

            await instructorImageService.AddAsync(instructorimage);
            await instructorImageService.SaveChangesAsync();

            return Success(localize.Get("ImageAddedSuccessfully"));

        }




        public async Task<Response<string>> Handle(UpdateInstructorImageCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = currentUserService.GetUserId();
            var instructor = await instructorsService.GetTableNoTracking()
                .FirstOrDefaultAsync(s => s.UserId == currentUserId);
            if (instructor == null)
                return Unauthorized<string>(localize.Get("InstructorNotFound"));
            // 1 Get image
            var image = await instructorImageService.GetTableAsTracking()
                        .FirstOrDefaultAsync(i => i.Id == request.ImageId
                        && i.InstructorId == instructor.Id);


            if (image == null)
                return NotFound<string>(localize.Get("ImageNotFound"));

            // Upload New Image

            var folderName = $"{UploadFolder.Instructor}/{instructor.Id}";

            var uploadResult = await fileService.UploadImageAsync(
                         request.Image,
                         folderName);

            if (uploadResult.Error != "Success")
            {
                return BadRequest<string>(localize.Get(uploadResult.Error));
            }


            //  Delete image

            if (!string.IsNullOrWhiteSpace(image.PublicId))
            {
                await fileService.DeleteImageAsync(image.PublicId);
            }


            // Update Database
            image.MediaPath = uploadResult.Url;
            image.PublicId = uploadResult.PublicId;


            await instructorImageService.UpdateAsync(image);
            await instructorImageService.SaveChangesAsync();

            return Success<string>(localize.Get("ImageUpdatedSuccessfully"));
        }



        //Delete Store Image 

        public async Task<Response<string>> Handle(DeleteInstructorImageCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = currentUserService.GetUserId();

            var instructor = await instructorsService.GetTableNoTracking()
                 .FirstOrDefaultAsync(s => s.UserId == currentUserId);

            if (instructor == null)
                return Unauthorized<string>("SellerNotFound");

            // 1️⃣ Get image
            var image = await instructorImageService.GetTableAsTracking()
                        .FirstOrDefaultAsync(i => i.Id == request.Id &&
                         i.Instructor.Id == instructor.Id);


            if (image == null)
                return NotFound<string>(localize.Get("ImageNotFound"));



            // 3️⃣ Delete image

            if (!string.IsNullOrWhiteSpace(image.PublicId))
            {
                await fileService.DeleteImageAsync(image.PublicId);
            }


            await instructorImageService.DeleteAsync(image);
            await instructorImageService.SaveChangesAsync();

            return Success<string>(localize.Get("ImageDeletedSuccessfully"));
        }





        #endregion
    }
}
