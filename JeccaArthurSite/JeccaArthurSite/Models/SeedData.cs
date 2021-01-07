using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Winterfell.Models
{
    public class SeedData
    {
        public static void Seed(MessageContext context)
        {
            if (!context.Messages.Any())
            {
                Message message = new Message
                {
                    Sender = new User { Name = "Mom" },
                    Recipient = new User { Name = "Misha" },
                    Subject = "Hello",
                    Body = "Bacon for breakfast this weekend",
                    Date = DateTime.Parse("12/3/20")
                };
                context.Messages.Add(message);

                message = new Message
                {
                    Sender = new User { Name = "Lucas" },
                    Recipient = new User { Name = "Jecca" },
                    Subject = "Groceries",
                    Body = "Add things to the list",
                    Date = DateTime.Parse("12/6/20")
                };
                context.Messages.Add(message);

                // next two messages are from the same user
                // stored in a variable so that both messages will be
                // associated with the same entity in the db

                User senderJecca = new User() { Name = "Jecca Arthur" };
                context.Users.Add(senderJecca);
                context.SaveChanges();   // adds a userID to the sender object

                message = new Message
                {
                    Sender = senderJecca,
                    Recipient = new User { Name = "Ben" },
                    Subject = "Congrats",
                    Body = "On your new truck!",
                    Date = DateTime.Parse("12/2/20")
                };
                context.Messages.Add(message);

                message = new Message
                {
                    Sender = senderJecca,
                    Recipient = new User { Name = "Alanna" },
                    Subject = "Interview",
                    Body = "Good luck today",
                    Date = DateTime.Parse("12/7/20")
                };
                context.Messages.Add(message);

                // save messages to db
                context.SaveChanges();
            }
        }
    }
}
