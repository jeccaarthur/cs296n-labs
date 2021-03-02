using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Winterfell.Models;

namespace Winterfell.Repositories
{
    public class ReplyRepository : IReplyRepository
    {
        private MessageContext context;

        public ReplyRepository(MessageContext c)
        {
            context = c;
        }

        public IQueryable<Reply> Replies
        {
            get
            {
                return context.Replies
                    .Include(reply => reply.User);
            }
        }

        public void AddReply(Reply reply)
        {
            context.Replies.Add(reply);
            context.SaveChanges();
        }

        public void DeleteReply(int id)
        {
            Reply reply = context.Replies.Where(reply => reply.ReplyID == id).SingleOrDefault();
            context.Remove(reply);
            context.SaveChanges();
        }

        public Reply GetReplyByID(int id)
        {
            Reply reply = context.Replies.Where(reply => reply.ReplyID == id)
                .Include(reply => reply.User).SingleOrDefault();

            return reply;
        }

        public void UpdateReply(Reply reply)
        {
            context.Replies.Update(reply);
            context.SaveChanges();
        }
    }
}
