using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

#nullable enable



namespace ourWinch.Models.Account
{
    public class ApplicationUser : IdentityUser
    {

        // 'Fornavn' is a required property representing the user's first name.
        [Required]
        public string Fornavn { get; set; }


        // 'Etternavn' is a required property representing the user's last name.
        [Required]
        public string Etternavn { get; set; }


        // 'MellomNavn' is an optional property representing the user's middle name.
        public string? MellomNavn { get; set; }


        // 'Role' is a not-mapped property that holds the name of the user's role.
        // This property is not mapped to the database.
        [NotMapped]
        public string? Role { get; set; }


        // 'RoleId' is a not-mapped property that holds the ID of the user's role.
        // This property is also not mapped to the database.
        [NotMapped]
        public string? RoleId { get; set; }


        // 'RoleList' is a not-mapped property that holds a list of roles for selection,
        // It is not mapped to the database.
        [NotMapped]
        public IEnumerable<SelectListItem>? RoleList { get; set; }






    }
}
