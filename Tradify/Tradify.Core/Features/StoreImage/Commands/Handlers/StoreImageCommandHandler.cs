using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.ProductsImages.Commands.Models;
using Tradify.Core.Features.StoreImage.Commands.Models;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data;
using Tradify.Data.Entities;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;


namespace Tradify.Core.Features.StoreImage.Commands.Handlers
{
    public class StoreImageCommandHandler : ResponseHandler
        , IRequestHandler<AddStoreImageCommand, Response<string>>
        //, IRequestHandler<UpdateStoreImageCommand, Response<string>>
        , IRequestHandler<DeleteStoreImageCommand, Response<string>>


    {

        #region Fields
        private readonly LocalizationService localize;
        private readonly IStoreService storeService;
        private readonly IFileService fileService;
        private readonly IStoreImageService storeImageService;
        private readonly IMapper mapper;
        private readonly ICurrentUserService currentUserService;
        private readonly ISellerService sellerService;


        #endregion

        #region Constructor
        public StoreImageCommandHandler(IStoreService storeService,
                                     IMapper mapper,
                                     IStoreImageService storeImageService,
                                     IFileService fileService,
                                     ISellerService sellerService,
                                     ICurrentUserService currentUserService,
                                     LocalizationService localize) : base(localize)
        {
            this.storeService = storeService;
            this.mapper = mapper;
            this.fileService = fileService;
            this.storeImageService = storeImageService;
            this.localize = localize;
            this.currentUserService = currentUserService;
            this.sellerService = sellerService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddStoreImageCommand request, CancellationToken cancellationToken)
        {



            // 2️⃣ Get Store
            var store = await storeService.GetTableAsTracking().IgnoreQueryFilters()
                     .FirstOrDefaultAsync(s => s.Id == request.StoreId);




            if (store == null)
                return NotFound<string>(localize.Get("StoreNotFound"));

            if (store.IsDeleted)
                return NotFound<string>(localize.Get("StoreIsDleated"));


            if (store.StoreImage != null)
                return BadRequest<string>(localize.Get("StoreHasImageAlrady"));




            //// 3️⃣ Upload image
            //var imagePath = await fileService.UploadGenericAsync(
            //UploadFolder.Store,
            //request.StoreId,
            //request.Image);

            //if (!imagePath.StartsWith("/"))
            //{
            //    return BadRequest<string>(localize.Get(imagePath));
            //}




            var folderName = $"{UploadFolder.Store}/{request.StoreId}";

            var uploadResult = await fileService.UploadImageAsync(
                         request.Image,
                         folderName);

            if (uploadResult.Error != "Success")
            {
                return BadRequest<string>(localize.Get(uploadResult.Error));
            }


            // 5️⃣ Save in DB
            var storeImage = new Data.Entities.StoreImage
            {
                StoreId = request.StoreId,
                MediaPath = uploadResult.Url,
                PublicId = uploadResult.PublicId
            };

            await storeImageService.AddAsync(storeImage);
            await storeImageService.SaveChangesAsync();

            return Success(localize.Get("ImageAddedSuccessfully"));

        }




        //public async Task<Response<string>> Handle(UpdateStoreImageCommand request, CancellationToken cancellationToken)
        //{
        //    var currentUserId = currentUserService.GetUserId();
        //    var seller = await sellerService.GetTableNoTracking()
        //        .FirstOrDefaultAsync(s => s.UserId == currentUserId);
        //    // 1️⃣ Get image
        //    var image = await storeImageService.GetTableAsTracking()
        //                .Include(s => s.Stores)
        //                .FirstOrDefaultAsync(i => i.Id == request.ImageId &&
        //                 i.Stores.SellerId == seller.Id);


        //    if (image == null)
        //        return NotFound<string>(localize.Get("ImageNotFound"));




        //    if (request.IsMain && !image.IsMain)
        //    {
        //        var oldMainImages = await storeImageService.GetTableAsTracking()
        //            .Where(i => i.StoreId == image.StoreId && i.IsMain)
        //           .ExecuteUpdateAsync(setters =>
        //                setters.SetProperty(i => i.IsMain, false));
        //    }

        //    // 4️⃣ Update fields
        //    image.IsMain = request.IsMain;
        //    image.SortOrder = request.SortOrder;

        //    await storeImageService.UpdateAsync(image);
        //    await storeImageService.SaveChangesAsync();

        //    return Success<string>(localize.Get("ImageUpdatedSuccessfully"));
        //}



        //Delete Store Image 

        public async Task<Response<string>> Handle(DeleteStoreImageCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = currentUserService.GetUserId();
            // var currentUserId = request.SellerId;


            var seller = await sellerService.GetTableNoTracking()
                 .FirstOrDefaultAsync(s => s.UserId == currentUserId);

            if (seller == null)
                return Unauthorized<string>("SellerNotFound");

            // 1️⃣ Get image
            var image = await storeImageService.GetTableAsTracking()
                        .Include(s => s.Stores)
                        .FirstOrDefaultAsync(i => i.Id == request.Id &&
                         i.Stores.SellerId == seller.Id);


            if (image == null)
                return NotFound<string>(localize.Get("ImageNotFound"));



            // 3️⃣ Delete image

            if (!string.IsNullOrWhiteSpace(image.PublicId))
            {
                await fileService.DeleteImageAsync(image.PublicId);
            }


            //if (!string.IsNullOrWhiteSpace(image.MediaPath))
            //{
            //    await fileService.DeleteFile(image.MediaPath);
            //}

            await storeImageService.DeleteAsync(image);
            await storeImageService.SaveChangesAsync();

            return Success<string>(localize.Get("ImageDeletedSuccessfully"));
        }





        #endregion
    }
}

