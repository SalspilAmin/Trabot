using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Messages.Commands.Handler
{
    public class MessageCommandHandler : ResponseHandler
    {
        public MessageCommandHandler(LocalizationService localization) : base(localization)
        {

        }
    }
}
