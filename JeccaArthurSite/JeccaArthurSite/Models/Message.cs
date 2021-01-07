using System;

namespace Winterfell.Models
{
    public class Message
    {
        public Message()
        {
        }

        public int MessageID { get; set; }

        public User Sender { get; set; }

        public User Recipient { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; }
    }
}
