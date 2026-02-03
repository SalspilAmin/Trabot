using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Helpers;
using Tradify.Service.AbstractsServices.IdentityServices;
using Tradify.Service.AbstractsServices.WatsappServices;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Tradify.Service.Services.WatsappServices
{
    public class WatsappService : IWatsappService
    {
        private readonly TwilioSettings twilioSettings;
        private readonly IUserService userService;
        public WatsappService( TwilioSettings twilioSettings , IUserService userService)
        {
            this.userService=userService;
             this.twilioSettings = twilioSettings;
            Twilio.TwilioClient.Init(twilioSettings.AccountSID, twilioSettings.AuthToken);
        }
        public async Task<bool> SendVerificationCodeAsync(string phoneNumber, string code)
        {

            try
            {
                var isnumber = userService.IsPhone(phoneNumber);
                if (!isnumber) return false;
                var message = await MessageResource.CreateAsync(
                   from: new PhoneNumber($"whatsapp:{twilioSettings.WhatsAppFrom}"),
                to: new PhoneNumber($"whatsapp:{phoneNumber}"),
                body: $"Your verification code is: {code}"
                    )  ;

                  return !string.IsNullOrEmpty(message.Sid);

            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
