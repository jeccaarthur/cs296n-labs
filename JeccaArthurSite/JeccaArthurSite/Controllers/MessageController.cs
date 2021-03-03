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
    public class MessageController : ControllerBase
    {
        IMessageRepository repository;
        UserManager<AppUser> userManager;

        public MessageController(UserManager<AppUser> u, IMessageRepository r)
        {
            repository = r;
            userManager = u;
        }

        // get all messages
        [HttpGet]
        public IActionResult GetMessages()
        {
            List<Message> messages = repository.Messages.ToList();

            if (messages.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(messages);
            }
        }

        // get one message by ID
        [HttpGet]
        public IActionResult GetMessageByID(int id)
        {
            Message message = repository.GetMessageByID(id);

            if (message == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(message);
            }
        }

        // add a message
        [HttpPost]
        public IActionResult AddMessage(Message message)
        {
            if (message != null)
            {
                message.Sender = userManager.GetUserAsync(User).Result;
                repository.AddMessage(message);
                return Ok(message);
            }
            else
            {
                return BadRequest();
            }
        }

        // replace a message
        [HttpPut]
        public IActionResult ReplaceMessage(int id, Message m)
        {
            Message message = repository.GetMessageByID(id);

            if (message.MessageID == id)
            {
                message.Sender = m.Sender;
                message.Recipient = m.Recipient;
                message.Subject = m.Subject;
                message.Body = m.Body;
                message.Date = m.Date;

                repository.UpdateMessage(message);

                return Ok(message);
            }
            else
            {
                return BadRequest();
            }
        }

        // delete a message
        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            Message message = repository.GetMessageByID(id);

            if (message != null)
            {
                repository.DeleteMessage(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // update a message
        [HttpPatch("{id}")]
        public IActionResult UpdateMessage(int id, string op, string path, string value)
        {
            return BadRequest();
        }
    }
}
