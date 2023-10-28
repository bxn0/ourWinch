using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ourWinch.Models.Account
{
    public class RegisterViewModel
    {

        [Required]
        public string Fornavn { get; set; }

        public string? MellomNavn { get; set; }

        [Required]
        public string Etternavn { get; set; }

        [Required]
        public string MobilNo { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

       // public IEnumerable<SelectListItem> RoleList { get; set; }

        public string Role { get; set; }




    }
}
