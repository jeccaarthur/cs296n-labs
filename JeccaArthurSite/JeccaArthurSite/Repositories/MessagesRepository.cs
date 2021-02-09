﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Winterfell.Models;

namespace Winterfell.Repositories
{
    public class MessagesRepository : IMessages
    {
        private MessageContext context;

        public MessagesRepository(MessageContext c)
        {
            context = c;
        }

        public IQueryable<Message> Messages
        {
            get
            {
                return context.Messages
                    .Include(message => message.Sender)
                    .Include(message => message.Recipient)
                    .Include(message => message.Comments)
                    .ThenInclude(comment => comment.Commenter);
            }
        }

        public void AddMessage(Message message)
        {
            context.Messages.Add(message);
            context.SaveChanges();
        }

        public void AddUser(Message message, AppUser user)
        {
            throw new NotImplementedException();
        }

        public Message GetMessageBySender(AppUser sender)
        {
            throw new NotImplementedException();
        }

        public void UpdateMessage(Message message)
        {
            context.Messages.Update(message);
            context.SaveChanges();
        }
    }
}
