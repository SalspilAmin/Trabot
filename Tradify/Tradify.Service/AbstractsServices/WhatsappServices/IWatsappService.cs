using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Service.AbstractsServices.WhatsappServices
{
    public interface IWatsappService
    {
        Task<bool> SendVerificationCodeAsync(string phoneNumber, string code);
    }
}
