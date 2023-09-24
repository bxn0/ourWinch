using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    [Required]
    public string? Mobil { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}
