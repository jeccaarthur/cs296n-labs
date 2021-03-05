using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Winterfell.Models
{
    public class Message
    {
        private List<Reply> replies = new List<Reply>();

        public int MessageID { get; set; }

        public AppUser Sender { get; set; }

        public AppUser Recipient { get; set; }

        [Required(ErrorMessage = "A subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message body is required")]
        [StringLength(500, ErrorMessage = "Message cannot exceed 500 characters")]
        public string Body { get; set; }

        public DateTime Date { get; set; }

        public List<Reply> Replies { get { return replies; } }
    }
}
