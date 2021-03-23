using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TemplateProject.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name ")]

        public string Name { get; set; }

        [Required]
        [Display(Name ="Category Description ")]
        public string catDescription { get; set; }


        public virtual ICollection<Job> Jobs { get; set; }

    }
}