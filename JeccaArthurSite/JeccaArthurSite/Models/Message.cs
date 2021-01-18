﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Winterfell.Models
{
    public class Message
    {
        public int MessageID { get; set; }

        public AppUser Sender { get; set; }

        public AppUser Recipient { get; set; }

        [Required(ErrorMessage = "A subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message body is required")]
        [StringLength(500, ErrorMessage = "Message cannot exceed 500 characters")]
        public string Body { get; set; }

        public DateTime Date { get; set; }
    }
}
