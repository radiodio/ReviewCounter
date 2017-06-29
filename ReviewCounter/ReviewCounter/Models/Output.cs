using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewCounter.Models
{
    public class Output
    {
        public int OutputId { get; set; }
        [Display(Name = "成果物")]
        public string ProcessOutput { get; set; }
    }
}
