using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewCounter.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        [Display(Name = "案件名")]
        public string Name { get; set; }
    }
}
