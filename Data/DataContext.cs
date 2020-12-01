using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data.Entities
{
    public class DataContext : DbContext
    {
        public DbSet<Vehicle> Vehicle { get; set; }
    }
}
