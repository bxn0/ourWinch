using System.ComponentModel.DataAnnotations;

public class ResetPasswordViewModel
{
    [Required]
    [DataType(DataType.Password)]
    public string? NewPassword { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "Wrong password.")]
    public string? ConfirmPassword { get; set; }
}
