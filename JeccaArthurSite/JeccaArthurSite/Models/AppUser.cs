using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winterfell.Models
{
    public class AppUser : IdentityUser
    {
        [Required(ErrorMessage = "User's name is required")]
        [StringLength(60, MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z]{1,60}$", ErrorMessage = "User's name must be alphabetic characters only")]
        public string Name { get; set; }

        [NotMapped]
        public IList<string> RoleNames { get; set; }
    }
}
