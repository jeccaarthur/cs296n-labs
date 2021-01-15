using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Winterfell.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "User's name is required")]
        [StringLength(60, MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z]{1,60}$", ErrorMessage = "User's name must be alphabetic characters only")]
        public string Name { get; set; }

        // public string Email { get; set; }
    }
}
