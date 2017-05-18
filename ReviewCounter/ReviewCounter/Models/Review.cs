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
        public DateTime Date { get; set; }
        public Member Reviewee { get; set; }
    }
}
