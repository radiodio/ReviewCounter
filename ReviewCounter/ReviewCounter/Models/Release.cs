using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewCounter.Models
{
    public class Release
    {
        public int ReleaseId { get; set; }
        [Display(Name = "バージョン")]
        public string Name { get; set; }
        public bool Closed { get; set; }
    }
}
