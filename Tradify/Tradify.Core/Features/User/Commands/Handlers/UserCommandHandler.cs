using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities.Identity;
using Tradify.Service.AbstractsServices.IdentityServices;

namespace Tradify.Core.Features.User.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,IRequestHandler<AddUserCommand,Response<string>>
        ,IRequestHandler<ChangeUserPasswordCommand,Response<string>>,IRequestHandler<DeleteUserCommand,Response<string>>    
    {
        #region Fildes
        private readonly LocalizationService localize;
        private readonly UserManager<Data.Entities.Identity.User> userManager;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        #endregion

        #region constructor

        public UserCommandHandler(LocalizationService localization,UserManager<Data.Entities.Identity.User>  _userManager,
            IMapper mapper,IUserService userService) : base(localization)
        {
            this.localize = localization;   
            this.userManager = _userManager;
            this.mapper = mapper;   
            this.userService = userService;

        }

       
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<Data.Entities.Identity.User>(request);

                var result = await userService.AddUserAsync(user, request.Password);
           
            switch(result.Item1)
            {

                case "EmailOrPhoneIsExist": return BadRequest<string>(localize.Get("EmailOrPhoneIsExist"));
                    break;
                case "UserNameIsExist": return BadRequest<string>(localize.Get("UserNameIsExist"));
                    break;
                case "Add_Correct_info": return BadRequest<string>(localize.Get("Add_Correct_info"));
                    break;
                case "Failed": return BadRequest<string>(localize.Get("TryToRegisterAgain"));

                    break;  
                case "Success": return Success<string>(result.Item1,meta:result.Item2);
                    break;
                default: return BadRequest<string>(result.Item1);
            }

        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            //CheckUser 
            var user =  userManager.Users.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Id == request.Id); 
            //not found
            if (user == null) return BadRequest<string>(localize.Get("UserIsNotFound"));


            // change password 
            var result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            //Succe or Faild
            if (result==null) return BadRequest<string>(result.Errors.FirstOrDefault()!.Description);
            return Success<string>(localize.Get("Success"));
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            // Check user
            var user = userManager.Users.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Id == request.Id);

            //if Not Exist notfound
            if (user == null) return NotFound<string>();
            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded) return BadRequest<string>(localize.Get("DeletedFailed"));
            return Success<string>(localize.Get("Deleted"));
        }

        #endregion
    }
}
