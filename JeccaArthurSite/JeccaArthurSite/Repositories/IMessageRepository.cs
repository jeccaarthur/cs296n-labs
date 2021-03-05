using System;
using System.Linq;
using Winterfell.Models;

namespace Winterfell.Repositories
{
    public interface IMessageRepository
    {
        IQueryable<Message> Messages { get; }
        void AddMessage(Message message);
        void AddUser(Message message, AppUser user);
        Message GetMessageBySender(AppUser sender);
        Message GetMessageByID(int id);
        void UpdateMessage(Message message);
        void DeleteMessage(int id);
    }
}
