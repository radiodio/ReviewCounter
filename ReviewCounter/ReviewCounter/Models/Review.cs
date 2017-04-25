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
        public int Time { get; set; }
        public string Memo { get; set; }
    }
}
