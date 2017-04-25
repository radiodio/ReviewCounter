using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ReviewCounter.Models
{
    public class ReviewCountingContext : DbContext
    {
        public ReviewCountingContext(DbContextOptions<ReviewCountingContext> options)
            : base(options)
        { }
    }
}
