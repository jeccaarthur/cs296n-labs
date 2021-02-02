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

                AppUser jonSnow = new AppUser { UserName = "jonSnow", Name = "Jon Snow" };
                AppUser dany = new AppUser { UserName = "dany", Name = "Daenerys Targaryen" };
                context.Users.Add(jonSnow);
                context.Users.Add(dany);
                context.SaveChanges();

                Message message = new Message
                {
                    Sender = jonSnow,
                    Recipient = dany,
                    Subject = "Welcome!",
                    Body = "I hope you enjoy Winterfell!",
                    Date = DateTime.Parse("11/1/2020")
                };
                context.Messages.Add(message);  // queues up the message to be added to the DB

                AppUser arya = new AppUser { UserName = "arya", Name = "Arya Stark" };
                AppUser brienne = new AppUser { UserName = "brienne", Name = "Brienne of Tarth" };
                context.Users.Add(arya);
                context.Users.Add(brienne);
                context.SaveChanges();

                message = new Message
                {
                    Sender = arya,
                    Recipient = brienne,
                    Subject = "Water dancing",
                    Body = "Meet me in the courtyard for practice",
                    Date = DateTime.Parse("11/15/2020")
                };
                context.Messages.Add(message);

                context.SaveChanges(); // stores the messages in the DB


                AppUser jeccaArthur = new AppUser()
                {
                    UserName = "jeccaarthur",
                    Name = "Jecca Arthur"
                };
                context.Users.Add(jeccaArthur);
                context.SaveChanges();
            }
        }
    }
}
