using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ReviewCounter.Models
{
    public class ReviewTime
    {
        public int Id { get; set; }
        public Review Review { get; set; }
        public Member Member { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int Time { get; set; }
    }
}
