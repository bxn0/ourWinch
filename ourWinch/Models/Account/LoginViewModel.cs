using System.ComponentModel.DataAnnotations;

/// <summary>
/// models a login view
/// </summary>
public class LoginViewModel
{



    /// <summary>
    /// Gets or sets the mobilno.
    /// </summary>
    /// <value>
    /// The mobilno.
    /// </value>
    [Phone]
    public string Mobilno { get; set; }



    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    /// <value>
    /// The password.
    /// </value>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }



    /// <summary>
    /// Gets or sets a value indicating whether [remember me].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [remember me]; otherwise, <c>false</c>.
    /// </value>
    public bool RememberMe { get; set; }


}
