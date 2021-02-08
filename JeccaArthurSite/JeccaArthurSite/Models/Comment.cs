using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winterfell.Models
{
    public class Comment
    {
        public int CommentID { get; set; }

        public string CommentText { get; set; }

        public DateTime CommentDate { get; set; }
    }
}
