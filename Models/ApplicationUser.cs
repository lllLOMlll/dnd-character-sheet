using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CharacterSheetDnD.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;
        
    }
}
