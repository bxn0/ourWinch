using System.ComponentModel.DataAnnotations;

public class ForgotPasswordViewModel
{
    [Required]
    [Phone]
    public string? Mobil { get; set; }
}
