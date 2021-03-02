using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winterfell.Models
{
    public class Reply
    {
        public int ReplyID { get; set; }

        public AppUser User { get; set; }

        public string ReplyText { get; set; }

        public DateTime Date { get; set; }
    }
}
