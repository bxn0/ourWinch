using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;



namespace ourWinch.Models.Account
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? Fornavn { get; set; }

        [Required]
        public string? Etternavn { get; set; }

        
        public string? MellomNavn { get; set; }
        [Required]
        public string? Password { get; set; }



    }
}
