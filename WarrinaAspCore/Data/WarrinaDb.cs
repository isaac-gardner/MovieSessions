using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarrinaAspCore.Models;

namespace WarrinaAspCore.Data
{
    public class WarrinaDb : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.Options != null)
                optionsBuilder.UseSqlite("Data Source=Warrina.db");
        }
    }
}
