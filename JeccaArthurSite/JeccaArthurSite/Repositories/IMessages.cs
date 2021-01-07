using System;
using System.Linq;
using Winterfell.Models;

namespace Winterfell.Repositories
{
    public interface IMessages
    {
        IQueryable<Message> Messages { get; }
        void AddMessage(Message message);
        void AddUser(Message message, User user);
        Message GetMessageBySender(string senderName);
        Message GetMessageByDate(string date);
    }
}
