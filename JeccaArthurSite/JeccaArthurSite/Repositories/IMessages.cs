using System;
using System.Linq;
using Winterfell.Models;

namespace Winterfell.Repositories
{
    public interface IMessages
    {
        IQueryable<Message> Messages { get; }
        void AddMessage(Message message);
        void AddUser(Message message, AppUser user);
        Message GetMessageBySender(AppUser sender);
        void UpdateMessage(Message message);
    }
}
