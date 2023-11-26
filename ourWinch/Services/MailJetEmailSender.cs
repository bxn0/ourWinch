using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using ourWinch.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ourWinch.Services
{

    /// <summary>
    /// A service for sending emails using the Mailjet API.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.UI.Services.IEmailSender" />
    public class MailJetEmailSender : IEmailSender
    {

        // Configuration object to access application settings.
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        // Options object to hold MailJet API credentials.
        /// <summary>
        /// The mail jet options
        /// </summary>
        public MailJetOptions _mailJetOptions;

        /// <summary>
        /// Initializes a new instance of the MailJetEmailSender class.
        /// </summary>
        /// <param name="configuration">The configuration object to access application settings.</param>
        public MailJetEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        /// <summary>
        /// Asynchronously sends an email using the Mailjet API.
        /// </summary>
        /// <param name="email">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="htmlMessage">The HTML message body of the email.</param>
        /// <returns>
        /// A Task representing the asynchronous operation.
        /// </returns>
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Retrieve MailJet configuration options from the application settings.
            _mailJetOptions = _configuration.GetSection("MailJet").Get<MailJetOptions>();


            // Initialize the Mailjet client with API credentials.
            MailjetClient client = new MailjetClient(_mailJetOptions.ApiKey, _mailJetOptions.SecretKey)
            {
                //Version = ApiVersion.V3_1,
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
             .Property(Send.Messages, new JArray {
             new JObject {
      {
       "From",
       new JObject {
        {"Email", "passse@proton.me"},
        {"Name", "Ben"}
       }
      }, {
       "To",
       new JArray {
        new JObject {
         {
          "Email",
          email
         }, {
          "Name",
          "Ben"
         }
        }
       }
      }, {
       "Subject",
      subject
      },  {
       "HTMLPart",
       htmlMessage
      },
     }
             });

            // Execute the request asynchronously using the Mailjet client.
            await client.PostAsync(request);
        }
    }
}
