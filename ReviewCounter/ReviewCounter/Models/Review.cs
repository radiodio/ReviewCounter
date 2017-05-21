using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewCounter.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public Project Project { get; set; }
        public int Ticket { get; set; }
        public Output Output { get; set; }
        public Member Reviewee { get; set; }
        public bool closed { get; set; }
    }
}
