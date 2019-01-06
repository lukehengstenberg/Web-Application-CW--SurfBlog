using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _878876.Models
{
    public class Post
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public DateTime PostDate { get; set; }
        
        [Required]
        [MinLength(5)]
        [MaxLength(3000)]
        public string Content { get; set; }

    }
}
