using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ourWinch.Models.Account
{
    /// <summary>
    /// Models the register view
    /// </summary>
    public class RegisterViewModel
    {


        
        /// <summary>
        /// Gets or sets the fornavn.
        /// 'Fornavn' is a required property representing the user's first name.
        /// 
        /// </summary>
        /// <value>
        /// The fornavn.
        /// </value>
        [Required]
        public string Fornavn { get; set; }


        
        /// <summary>
        /// Gets or sets the mellom navn.
        /// 'MellomNavn' is an optional property representing the user's middle name.
        /// </summary>
        /// <value>
        /// The mellom navn.
        /// </value>
        public string? MellomNavn { get; set; }


        
        /// <summary>
        /// Gets or sets the etternavn.
        /// 'Etternavn' is a required property representing the user's last name.
        /// </summary>
        /// <value>
        /// The etternavn.
        /// </value>
        [Required]
        public string Etternavn { get; set; }


        
        /// <summary>
        /// Gets or sets the mobil no.
        /// 'MobilNo' is a required property representing the user's mobile number.
        /// </summary>
        /// <value>
        /// The mobil no.
        /// </value>
        [Required]
        public string MobilNo { get; set; }


      
        /// <summary>
        /// Gets or sets the email.
        /// 'Email' is a required property and must be a valid email address.
        /// It is labeled as "Email" in the UI.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        
        /// <summary>
        /// Gets or sets the password.
        /// 'Password' is a required property with a string length requirement.
        ///  The password must be at least 6 characters long and no more than 100 characters.
        ///  It is treated as a password field in the UI and labeled as "Password".
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        // public IEnumerable<SelectListItem> RoleList { get; set; }


        
        /// <summary>
        /// Gets or sets the role.
        /// 'Role' represents the user's role. This field isn't required and doesn't have any specific annotations.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public string Role { get; set; }




    }
}
