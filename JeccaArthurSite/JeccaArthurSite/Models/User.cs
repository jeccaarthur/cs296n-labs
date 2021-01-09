﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Winterfell.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 1)]
        public string Username { get; set; }

        // public string Email { get; set; }
    }
}
