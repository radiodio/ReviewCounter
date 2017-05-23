using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewCounter.Models
{
    public class Release
    {
        public int ReleaseId { get; set; }
        public string Name { get; set; }
        public bool Closed { get; set; }
    }
}
