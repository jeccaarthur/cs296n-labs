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

        [HttpPut]
        {
    }
}
