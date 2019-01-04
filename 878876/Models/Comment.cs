using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _878876.Models
{
    public class Comment
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public DateTime CommentDate { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public virtual Post MyPost { get; set; }
    }
}
