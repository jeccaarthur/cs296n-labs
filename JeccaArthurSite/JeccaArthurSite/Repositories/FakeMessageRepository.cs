using System;
using System.Collections.Generic;
using System.Linq;
using Winterfell.Models;

namespace Winterfell.Repositories
{
    public class FakeMessageRepository : IMessageRepository
    {
        private List<Message> messages = new List<Message>();

        public FakeMessageRepository()
        {
        }

        public IQueryable<Message> Messages
        {
            get
            {
                return messages.AsQueryable<Message>();
            }
        }

        public void AddMessage(Message message)
        {
            // simulate AI PK
            message.MessageID = messages.Count;

            // add message to list
            messages.Add(message);
        }

        public void AddUser(Message message, AppUser user)
        {
            throw new NotImplementedException();
        }

        public void DeleteMessage(int id)
        {
            throw new NotImplementedException();
        }

        public Message GetMessageByID(int id)
        {
            throw new NotImplementedException();
        }

        public Message GetMessageBySender(AppUser sender)
        {
            throw new NotImplementedException();
        }

        public void UpdateMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
