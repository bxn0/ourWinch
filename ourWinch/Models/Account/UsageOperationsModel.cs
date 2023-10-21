using System.ComponentModel.DataAnnotations;

namespace ourWinch.Models.Account
{
    public class RegistrationModel
    {
        [Required]
        public string? Fornavn { get; set; }

        [Required]
        public string? Etternavn { get; set; }

        [Required]
        public string? MobilNo { get; set; }

        public string? MellomNavn { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public string? Role { get; set; }
    }
}