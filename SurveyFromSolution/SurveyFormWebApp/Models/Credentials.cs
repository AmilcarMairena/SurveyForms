﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Models
{
    public class Credentials
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
