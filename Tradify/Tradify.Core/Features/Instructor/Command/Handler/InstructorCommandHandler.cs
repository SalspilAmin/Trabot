using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Education.Command.Models;
using Tradify.Core.Features.Instructor.Command.Models;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.Store.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Appointments;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Enums;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.AuthorizationServices;
using Tradify.Service.AbstractsServices.IdentityServices;
using Tradify.Service.Services;
using Tradify.Service.Services.IdentityServices;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Core.Features.Instructor.Command.Handler
{
    public class InstructorCommandHandler : ResponseHandler,
                                         IRequestHandler<AddInstructorCommand, Response<string>>,
                                         IRequestHandler<AddInstructorWithImageCommand, Response<string>>,
                                         IRequestHandler<UpdateInstructoreCommand, Response<string>>,
                                         IRequestHandler<DisActiveInstructorCommand, Response<string>>,
                                         IRequestHandler<ActiveInstructorCommand, Response<string>>






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
        private readonly IUserService userService;
        private readonly UserManager<Data.Entities.Identity.User> userManager;


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
                                    , IInstructorImageService instructorImageService
            , UserManager<Data.Entities.Identity.User> _userManager, IUserService userService) : base(localize)
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
            this.userService = userService;
            this.userManager = _userManager;

        }
        #endregion

        #region Methods

        // Add Instructor

      
        public async Task<Response<string>> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {

            var user = mapper.Map<Data.Entities.Identity.User>(request);

            var userResult = await userService.AddUserAsync(user, request.Password);

            switch (userResult.Item1)
            {

                case "EmailOrPhoneIsExist":
                    return BadRequest<string>(localize.Get("EmailOrPhoneIsExist"));
                    break;
                case "UserNameIsExist":
                    return BadRequest<string>(localize.Get("UserNameIsExist"));
                    break;
                case "Add_Correct_info":
                    return BadRequest<string>(localize.Get("Add_Correct_info"));
                    break;
                case "Failed":
                    return BadRequest<string>(localize.Get("TryToRegisterAgain"));

                    break;

                case "Success":
                    break;

                default:
                    return BadRequest<string>(userResult.Item1);
            }

            var UserId = userResult.Item2.Value;

            var instructor = mapper.Map<Instructors>(request);


            var result = await instructorsService.AddInstructorAsync(instructor, UserId);

            if (result.Item1 != "Success")
            {
                var createdUser = await userManager.FindByIdAsync(UserId.ToString());

                if (createdUser != null)
                {
                    await userManager.DeleteAsync(createdUser);
                }


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

            var user = mapper.Map<Data.Entities.Identity.User>(request);

            var userResult = await userService.AddUserAsync(user, request.Password);

            switch (userResult.Item1)
            {

                case "EmailOrPhoneIsExist":
                    return BadRequest<string>(localize.Get("EmailOrPhoneIsExist"));
                    break;
                case "UserNameIsExist":
                    return BadRequest<string>(localize.Get("UserNameIsExist"));
                    break;
                case "Add_Correct_info":
                    return BadRequest<string>(localize.Get("Add_Correct_info"));
                    break;
                case "Failed":
                    return BadRequest<string>(localize.Get("TryToRegisterAgain"));

                    break;
            }
            var UserId = userResult.Item2.Value;

            var instructor = mapper.Map<Instructors>(request);


            var result = await instructorsService.AddInstructorAsync(instructor, UserId);

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
            var instructorImage = new Data.Entities.Appointments.InstructorImage
            {
                InstructorId = instructorid,
                MediaPath = uploadResult.Url,
                PublicId = uploadResult.PublicId
            };


            await instructorImageService.AddAsync(instructorImage);
            await instructorImageService.SaveChangesAsync();

            return Success<string>("Success", meta: instructorid);


        }


        // Update instructor  

        public async Task<Response<string>> Handle(UpdateInstructoreCommand request, CancellationToken cancellationToken)
        {
            var curantUser = currentUserService.GetUserId();
            var instructor = await instructorsService.GetTableNoTracking()
                .FirstOrDefaultAsync(i => i.UserId == curantUser);

            if (instructor == null)
                return BadRequest<string>(localize.Get("InstructorNotFound"));



            instructor.Name = request.Name;
            instructor.JobTitle = request.JobTitle;
            instructor.About = request.About;
            instructor.PricePerSession = request.PricePerSession;
            instructor.YearsOfExperience = request.YearsOfExperience;


            await instructorsService.SaveChangesAsync();

            return Success<string>(localize.Get("InstructorUpdatedSuccessfully"));
        }


        // Dis Active instructor  

        public async Task<Response<string>> Handle(DisActiveInstructorCommand request, CancellationToken cancellationToken)
        {
            //  1. Check if seller exist
            var ValidSeller = await currentUserService.GetValidSellerContextAsync();

            if (ValidSeller.Error != null)
                return BadRequest<string>(localize.Get(ValidSeller.Error));


            // 2. Get Seller , Store
            var seller = ValidSeller.Seller;
            var store = ValidSeller.Store;

            //3. Cheack If Store Type Is Service 

            if (store.Type != StoreType.Service)
                return BadRequest<string>(localize.Get("ThisActionAllowedForServiceStoresOnly"));

            var instructor = await instructorsService.GetTableAsTracking()
                .FirstOrDefaultAsync(i=>i.Id==request.Id
                &&i.StoreId== store.Id);

            if (instructor == null)
                return BadRequest<string>(localize.Get("InstructorNotFound"));

            if (!instructor.IsActive)
                return BadRequest<string>(localize.Get("InstructorIsAlreadyNotActive"));


            instructor.IsActive = false;


            await instructorsService.SaveChangesAsync();

            return Success<string>(localize.Get("InstructorDisActiveSuccessfully"));
        }

        // Active instructor  

        public async Task<Response<string>> Handle(ActiveInstructorCommand request, CancellationToken cancellationToken)
        {
            //  1. Check if seller exist
            var ValidSeller = await currentUserService.GetValidSellerContextAsync();

            if (ValidSeller.Error != null)
                return BadRequest<string>(localize.Get(ValidSeller.Error));


            // 2. Get Seller , Store
            var seller = ValidSeller.Seller;
            var store = ValidSeller.Store;

            //3. Cheack If Store Type Is Service 

            if (store.Type != StoreType.Service)
                return BadRequest<string>(localize.Get("ThisActionAllowedForServiceStoresOnly"));

            var instructor = await instructorsService.GetTableAsTracking().IgnoreQueryFilters()
                .FirstOrDefaultAsync(i => i.Id == request.Id
                && i.StoreId == store.Id);

            if (instructor == null)
                return BadRequest<string>(localize.Get("InstructorNotFound"));

            if (instructor.IsActive)
                return BadRequest<string>(localize.Get("InstructorIsAlreadyActive"));


            instructor.IsActive = true;


            await instructorsService.SaveChangesAsync();

            return Success<string>(localize.Get("InstructorActiveSuccessfully"));
        }
        #endregion
    }
}