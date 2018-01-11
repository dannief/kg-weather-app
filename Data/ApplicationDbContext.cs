using KG.Weather.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KG.Weather.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public DbSet<Worker> Workers { get; set; }

        public DbSet<City> Cities { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Worker>()
                .OwnsOne(x => x.Type)
                .Property(x => x.Value).HasColumnName("Type").HasMaxLength(16);
           
            modelBuilder.Entity<City>()
                .OwnsOne(x => x.Country)
                .Property(x => x.Value).HasColumnName("Country").HasMaxLength(32);           
        }
    }
}
