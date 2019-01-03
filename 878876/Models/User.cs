using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _878876.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(4), MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        [MinLength(2), MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2), MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
