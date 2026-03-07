using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Tradify.Data.Helpers;
using Tradify.Service.AbstractsServices.WhatsappServices;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Tradify.Service.Services.WhatsappServices
{
    public class WatsappService : IWatsappService
    {
        private readonly TwilioSettings twilioSettings;

    public WatsappService(TwilioSettings twilioSetting)
    {

        this.twilioSettings = twilioSetting;
        Twilio.TwilioClient.Init(twilioSettings.AccountSID, twilioSettings.AuthToken);
    }
    public bool IsPhone(string input)
    {
        input = input.Replace(" ", "").Trim();
        return Regex.IsMatch(input, @"^(?:\+20|0)?1[0125][0-9]{8}$");
    }
    public async Task<bool> SendVerificationCodeAsync(string phoneNumber, string code)
    {

        try
        {
            var isnumber = IsPhone(phoneNumber);
            if (!isnumber) return false;
            var message = await MessageResource.CreateAsync(
               from: new PhoneNumber($"whatsapp:{twilioSettings.WhatsAppFrom}"),
            to: new PhoneNumber($"whatsapp:{phoneNumber}"),
            body: $"Your verification code is: {code}"
                );

            return !string.IsNullOrEmpty(message.Sid);

        }
        catch (Exception ex)
        {

            return false;
        }
    }
}
}
