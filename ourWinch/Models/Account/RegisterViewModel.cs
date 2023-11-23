using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ourWinch.Models.Account
{
    public class RegisterViewModel
    {


        // 'Fornavn' is a required property representing the user's first name.

        [Required]
        public string Fornavn { get; set; }


        // 'MellomNavn' is an optional property representing the user's middle name.
        public string? MellomNavn { get; set; }


        // 'Etternavn' is a required property representing the user's last name.
        [Required]
        public string Etternavn { get; set; }


        // 'MobilNo' is a required property representing the user's mobile number.
        [Required]
        public string MobilNo { get; set; }


        // 'Email' is a required property and must be a valid email address.
        // It is labeled as "Email" in the UI.
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        // 'Password' is a required property with a string length requirement.
        // The password must be at least 6 characters long and no more than 100 characters.
        // It is treated as a password field in the UI and labeled as "Password".
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        // public IEnumerable<SelectListItem> RoleList { get; set; }


        // 'Role' represents the user's role. This field isn't required and doesn't have any specific annotations.
        public string Role { get; set; }




    }
}
