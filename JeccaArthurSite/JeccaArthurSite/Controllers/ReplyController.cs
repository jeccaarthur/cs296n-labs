using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Winterfell.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReplyController : ControllerBase
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // get all replies
        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = bookRepo.GetAllBooks();
            if (books.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(books);
            }
        }


    }
}
