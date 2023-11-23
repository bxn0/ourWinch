using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{


    // 'Mobilno' represents the mobile number of the user. It's validated as a phone number.
    // While not marked as 'Required', it's typically essential for logging in.
    [Phone]
    public string Mobilno { get; set; }


    // 'Password' is a required field and is treated as a password data type, 
    // which means it can be used to input passwords in a secure manner in the UI.
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }


    // 'RememberMe' is a boolean indicating whether the user's login information 
    // should be remembered by the application, typically for convenience in future logins.
    public bool RememberMe { get; set; }


}
