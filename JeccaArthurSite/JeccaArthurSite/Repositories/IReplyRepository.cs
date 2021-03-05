using System;
using System.Linq;
using Winterfell.Models;

namespace Winterfell.Repositories
{
    public interface IReplyRepository
    {
        IQueryable<Reply> Replies { get; }
        void AddReply(Reply reply);
        Reply GetReplyByID(int id);
        void UpdateReply(Reply reply);
        void DeleteReply(int id);
    }
}
