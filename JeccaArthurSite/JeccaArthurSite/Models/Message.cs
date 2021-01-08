using System;
using System.ComponentModel.DataAnnotations;

namespace Winterfell.Models
{
    public class Message
    {
        public int MessageID { get; set; }

        public User Sender { get; set; }

        public User Recipient { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTime Date { get; set; }
    }
}
