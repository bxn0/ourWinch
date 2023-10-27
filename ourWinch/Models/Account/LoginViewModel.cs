using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    [Phone]
    public string Mobilno { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public bool RememberMe { get; set; }


}
