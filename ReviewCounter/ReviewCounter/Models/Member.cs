using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ReviewCounter.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        [Display(Name = "メンバー")]
        public string Name { get; set; }
    }
}
