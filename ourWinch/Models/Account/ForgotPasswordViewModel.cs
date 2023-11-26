using System.ComponentModel.DataAnnotations;

/// <summary>
/// Models forgorpasswordview
/// </summary>
public class ForgotPasswordViewModel
{

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    /// <value>
    /// The email.
    /// </value>
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

