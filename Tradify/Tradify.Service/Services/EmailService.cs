using System;
using System.Collections.Generic;
using MailKit.Net.Smtp;
using MimeKit;
using System.Text;
using Tradify.Data.Helpers;
using Tradify.Service.AbstractsServices;
using Org.BouncyCastle.Asn1.Cms;

namespace Tradify.Service.Services
{
    public class EmailService : IEmailService
    {
        #region Fields
        private readonly EmailSettings _emailSettings;

        
        #endregion

        #region Constructors
        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }


        #endregion

        #region Methods


        public async Task<string> SendEmail(string email, string Message, string? reason)
        {
            try
            {

                using (var client = new SmtpClient()) { 
                    //connect to server
                await client.ConnectAsync(_emailSettings.Host,_emailSettings.Port,true);
                    //Authenticated  to server
                 await   client.AuthenticateAsync(_emailSettings.FromEmail,_emailSettings.Password);

                    var BodyBuilder = new BodyBuilder() {

                        HtmlBody =$"{Message}",
                        TextBody= "Wellocome"
                    
                    };

                    var message = new MimeMessage()
                    {
                        Body = BodyBuilder.ToMessageBody()
                    };
                    message.From.Add(new MailboxAddress("technical support", _emailSettings.FromEmail));
                    message.To.Add(new MailboxAddress("Testing",email));
                    message.Subject= reason==null ?  "No Submitted" : reason;
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);

                }


                return "Success";

            }

            catch ( Exception ex)
            { 

                return "Failed";

            }


        }
        #endregion




    }
}
