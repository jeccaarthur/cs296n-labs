using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Winterfell.Models;
using Winterfell.Repositories;

namespace Winterfell.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReplyController : ControllerBase
    {
        IReplyRepository repository;
        UserManager<AppUser> userManager;

        public ReplyController(UserManager<AppUser> u, IReplyRepository r)
        {
            repository = r;
            userManager = u;
        }

        // get all replies
        [HttpGet]
        public IActionResult GetReplies()
        {
            List<Reply> replies = repository.Replies.ToList();

            if (replies.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(replies);
            }
        }

        // get one reply by ID
        [HttpGet("{id}")]
        public IActionResult GetReplyByID(int id)
        {
            Reply reply = repository.GetReplyByID(id);

            if (reply == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(reply);
            }
        }

        // add a reply
        [HttpPost]
        public IActionResult AddReply(Reply reply)
        {
            if (reply != null)
            {
                repository.AddReply(reply);
                return Ok(reply);
            }
            else
            {
                return BadRequest();
            }
        }

        // replace a reply
        [HttpPut("{id}")]
        public IActionResult ReplaceReply(int id, Reply r)
        {
            Reply reply = repository.GetReplyByID(id);

            if (reply.ReplyID == id)
            {
                reply.User = r.User;
                reply.ReplyText = r.ReplyText;
                reply.Date = r.Date;

                repository.UpdateReply(reply);

                return Ok(reply);
            }
            else
            {
                return BadRequest();
            }
        }

        // delete a reply
        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            Reply reply = repository.GetReplyByID(id);

            if (reply != null)
            {
                repository.DeleteReply(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // update a reply
        [HttpPatch("{id}")]
        public IActionResult UpdateReply(int id, string op, string path, string value)
        {
            Reply reply = repository.GetReplyByID(id);

            switch (path)
            {
                case "subject":
                    reply.ReplyText = value;
                    break;
                case "date":
                    reply.Date = Convert.ToDateTime(value);
                    break;
                default:
                    return BadRequest();
            }

            repository.UpdateReply(reply);

            return Ok(reply);
        }
    }
}
