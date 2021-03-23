using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TemplateProject.Models
{
    public class JobsApplyViewModel
    {
        public IEnumerable<Job> Jobs { get; set; }
        public IEnumerable<ApplyJob> ApplyJobs { get; set; }

    }
}