using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _878876.Models;

namespace _878876.ViewModels
{
    public class PostCommentsViewModel
    {
        public Post Post { get; set; }
        public List<Comment> Comments { get; set; }

        public int PostID { get; set; }
        public string Author { get; set; }
        public DateTime CommentDate { get; set; }
        public string Content { get; set; }
    }
}
