using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Service.AbstractsServices
{
    public interface IEmailService
    {

        public Task<string> SendEmail(string email, string Message, string? reason);


    }
}
