namespace ourWinch.Models
{
    /// <summary>
    /// models the error view
    /// </summary>
    public class ErrorViewModel

    {/// <summary>
     /// Gets or sets the request ID. Can be null or empty.
     /// </summary>
        public string? RequestId { get; set; }

        // <summary>
        /// <summary>
        /// Gets a value indicating whether [show request identifier].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show request identifier]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}