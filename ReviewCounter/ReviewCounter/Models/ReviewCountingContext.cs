using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReviewCounter.Models;

namespace ReviewCounter.Models
{
    public class ReviewCountingContext : DbContext
    {
        public ReviewCountingContext(DbContextOptions<ReviewCountingContext> options)
            : base(options)
        { }
        public DbSet<ReviewCounter.Models.Review> Review { get; set; }
        public DbSet<ReviewCounter.Models.Project> Project { get; set; }
        public DbSet<ReviewCounter.Models.Member> Member { get; set; }
        public DbSet<ReviewCounter.Models.ReviewTime> ReviewTime { get; set; }
        public DbSet<ReviewCounter.Models.Output> Output { get; set; }
    }
}
