using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TemplateProject.Models
{
    public class ApplyJob
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int JobId { get; set; }

        public string Message { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("JobId")]
        public virtual Job Job { get; set; }
    }
}