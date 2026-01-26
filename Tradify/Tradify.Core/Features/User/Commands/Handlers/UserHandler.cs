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
    public class UserHandler : ResponseHandler,IRequestHandler<AddUserCommand,Response<string>>
    {
        #region Fildes
        private readonly LocalizationService localize;
        private readonly UserManager<Data.Entities.Identity.User> userManager;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        #endregion

        #region constructor

        public UserHandler(LocalizationService localization,UserManager<Data.Entities.Identity.User>  _userManager,
            IMapper mappe,IUserService userService) : base(localization)
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
            switch(result)
            {

                case "EmailOrPhoneIsExist": return BadRequest<string>(localize.Get("EmailOrPhoneIsExist"));
                    break;
                case "UserNameIsExist": return BadRequest<string>(localize.Get("UserNameIsExist"));
                    break;
                case "Failed": return BadRequest<string>(localize.Get("TryToRegisterAgain"));
                    break;  
                case "Success": return Success<string>("");
                    break;
                default: return BadRequest<string>(result);
            }

        }

        #endregion
    }
}
