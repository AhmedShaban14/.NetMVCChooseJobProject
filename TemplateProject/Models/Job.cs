using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TemplateProject.Models
{
    public class Job
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Enter Job Title")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Please Enter Job Content")]
        [AllowHtml]
        public string JobContent { get; set; }

        public string JobImage { get; set; }

        [Required(ErrorMessage = "Please Choose Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<ApplyJob> ApplyJob { get; set; }


    }
}