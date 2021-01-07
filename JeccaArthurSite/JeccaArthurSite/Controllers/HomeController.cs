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

namespace Winterfell.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        IMessages repository;

        // any controller that needs to access the db must have an instance of the context object
        // passed to its constructor as a param (MessageContext c)
        public HomeController(ILogger<HomeController> logger, IMessages r)
        {
            _logger = logger;

            repository = r;
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
        public IActionResult Message()
        {
            return View();
        }

        // return form input
        [HttpPost]
        public IActionResult Message(Message message)
        {
            message.Date = DateTime.Now;

            /* added to repository method AddMessage
            // add the model to the database and save changes
            context.Messages.Add(model);
            context.SaveChanges();
            */

            repository.AddMessage(message);

            return View(message);
        }

        // gets data from database
        public IActionResult Messages()
        {
            // use var - implicit variable
            // var messageList = context.Messages.Include(message => message.Sender)
            //     .Include(message => message.Recipient)
            //     .ToList<Message>();

            // use repository instead of pulling directly from the db ^
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
    }
}
