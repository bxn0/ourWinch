namespace ourWinch.Models
{
    public class ErrorViewModel

    {/// <summary>
     /// Gets or sets the request ID. Can be null or empty.
     /// </summary>
        public string? RequestId { get; set; }

        // <summary>
        /// Gets a value indicating whether the request ID should be shown.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}