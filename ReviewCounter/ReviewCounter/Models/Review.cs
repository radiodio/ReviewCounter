﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewCounter.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public Project Project { get; set; }
        public Release Version { get; set; }
        public int Ticket { get; set; }
        public Output Output { get; set; }
        [Display(Name = "レビュイ")]
        public Member Author { get; set; }
    }
}
