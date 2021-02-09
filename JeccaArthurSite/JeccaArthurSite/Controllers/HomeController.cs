using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Winterfell.Models;
using Winterfell.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Winterfell.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        IMessages repository;
        UserManager<AppUser> userManager;

        // any controller that needs to access the db must have an instance of the context object
        // passed to its constructor as a param (MessageContext c)
        public HomeController(ILogger<HomeController> logger, IMessages r, UserManager<AppUser> u)
        {
            _logger = logger;
            repository = r;
            userManager = u;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        // invoke the view with a form for entering a message
        [Authorize]
        public IActionResult Message()
        {
            return View();
        }

        // return form input
        [HttpPost]
        public IActionResult Message(Message message)
        {
            message.Sender = userManager.GetUserAsync(User).Result;
            // TODO: get the user's real name in registration and replace below
            message.Sender.Name = message.Sender.UserName;      // temporary hack
            
            message.Date = DateTime.Now;

            if (ModelState.IsValid)
            {
                repository.AddMessage(message);
            }

            return View(message);
        }

        // gets data from database
        public IActionResult Messages()
        {
            // pull messages from repository
            List<Message> messageList = repository.Messages.ToList<Message>();

            return View(messageList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public IActionResult Comment(int messageID)
        {
            var commentVM = new CommentVM { MessageID = messageID };

            return View(commentVM);
        }

        [HttpPost]
        public RedirectToActionResult Comment(CommentVM commentVM)
        {
            // Comment is the domain model
            var comment = new Comment { CommentText = commentVM.CommentText };
            comment.Commenter = userManager.GetUserAsync(User).Result;
            comment.CommentDate = DateTime.Now;

            // get the message that this comment is for
            Message message = new Message();
            message = repository.Messages.Where(m => m.MessageID == commentVM.MessageID).SingleOrDefault();

            // store the message with the comment in the database
            message.Comments.Add(comment);
            repository.UpdateMessage(message);

            return RedirectToAction("Messages");
        }
    }
}
