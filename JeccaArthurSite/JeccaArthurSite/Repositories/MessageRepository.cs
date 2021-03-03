using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Winterfell.Models;

namespace Winterfell.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private MessageContext context;

        public MessageRepository(MessageContext c)
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
                    .Include(message => message.Replies)
                    .ThenInclude(reply => reply.User);
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

        public Message GetMessageByID(int id)
        {
            Message message = context.Messages.Where(message => message.MessageID == id).SingleOrDefault();

            return message;
        }

        public void UpdateMessage(Message message)
        {
            context.Messages.Update(message);
            context.SaveChanges();
        }

        public void DeleteMessage(int id)
        {
            Message message = context.Messages.Where(message => message.MessageID == id).SingleOrDefault();

            context.Remove(message);
            context.SaveChanges();
        }
    }
}
