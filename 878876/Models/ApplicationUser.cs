using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _878876.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Location { get; set; }
        public DateTime Birthdate { get; set; }
        public string Name { get; set; }
    }
}
