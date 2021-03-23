using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TemplateProject.Models
{
    public class Contact
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [AllowHtml]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }
    }
}