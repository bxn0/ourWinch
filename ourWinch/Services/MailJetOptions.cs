namespace ourWinch.Services
{


    /// <summary>
    /// Represents the configuration options for MailJet API integration.
    /// </summary>
    public class MailJetOptions
    {
        /// <summary>
        /// Gets or sets the API key for MailJet.
        /// This key is used for authenticating requests to the MailJet API.
        /// </summary>
        /// <value>
        /// The API key.
        /// </value>
        public string ApiKey{ get; set; }

        /// <summary>
        /// Gets or sets the secret key for MailJet.
        /// This key is used in conjunction with the API key to authenticate requests.
        /// </summary>
        /// <value>
        /// The secret key.
        /// </value>
        public string SecretKey { get; set; }
    }
}
