using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Winterfell.Models
{
    public class SeedData
    {
        public static void Seed(MessageContext context, RoleManager<IdentityRole> roleManager)
        {            if (!context.Messages.Any())  // this is to prevent duplicate data from being added
            {
                // create member role
                // TODO: check the result to see if the role was successfully added
                var result = roleManager.CreateAsync(new IdentityRole("Member")).Result;

                Message message = new Message
                {
                    Sender = new AppUser { Name = "Jon Snow" },
                    Recipient = new AppUser { Name = "Daenerys Targaryen" },
                    Subject = "Welcome!",
                    Body = "I hope you enjoy Winterfell!",
                    Date = DateTime.Parse("11/1/2020")
                };
                context.Messages.Add(message);  // queues up the message to be added to the DB

                message = new Message
                {
                    Sender = new AppUser { Name = "Arya" },
                    Recipient = new AppUser { Name = "Brienne of Tarth" },
                    Subject = "Water dancing",
                    Body = "Meet me in the courtyard for practice",
                    Date = DateTime.Parse("11/15/2020")
                };
                context.Messages.Add(message);

                context.SaveChanges(); // stores the messages in the DB
            }
        }
    }
}
