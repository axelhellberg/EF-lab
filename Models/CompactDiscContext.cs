using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_lab.Models
{
    public class CompactDiscContext:DbContext
    {
        public CompactDiscContext(DbContextOptions<CompactDiscContext> options) : base(options)
        {

        }

        public DbSet<CompactDisc> Discs { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
