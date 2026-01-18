using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.User.Commands.Handlers
{
    public class UserHandler : ResponseHandler
    {
        private readonly LocalizationService localize;


        public UserHandler(LocalizationService localization) : base(localization)
        {
        }
    }
}
