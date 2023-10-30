﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
#nullable enable



namespace ourWinch.Models.Account
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Fornavn { get; set; }

        [Required]
        public string Etternavn { get; set; }

        
        public string? MellomNavn { get; set; }


        [NotMapped]
        public string Role { get; set; }






    }
}
