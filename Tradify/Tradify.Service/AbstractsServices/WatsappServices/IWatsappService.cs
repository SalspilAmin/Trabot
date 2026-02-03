using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Service.AbstractsServices.WatsappServices
{
    public interface IWatsappService
    {
        Task<bool> SendVerificationCodeAsync(string phoneNumber, string code);
    }
}
