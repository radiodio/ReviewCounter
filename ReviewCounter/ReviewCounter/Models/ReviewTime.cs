using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewCounter.Models
{
    public class ReviewTime
    {
        public int Id { get; set; }
        public Review Review { get; set; }
        public Member Member { get; set; }
        public int Time { get; set; }
    }
}
