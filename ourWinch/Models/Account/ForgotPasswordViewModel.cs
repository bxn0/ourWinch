using System.ComponentModel.DataAnnotations;

public class ForgotPasswordViewModel
{

    // 'Email' is a required property that must be a valid email address.
    // This is where the user will input their email address to receive the password reset link.
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

