﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _878876.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "User Claims")]
        public List<SelectListItem> UserClaims { get; set; }
    }
}
