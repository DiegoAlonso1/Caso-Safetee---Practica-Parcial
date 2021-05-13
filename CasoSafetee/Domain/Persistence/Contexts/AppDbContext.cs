using CasoSafetee.Domain.Models;
using CasoSafetee.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CasoSafetee.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Guardian> Guardians { get; set; }
        public DbSet<Urgency> Urgencies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Guardians
            builder.Entity<Guardian>().ToTable("Guardians");
            builder.Entity<Guardian>().HasKey(e => e.Id);
            builder.Entity<Guardian>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Guardian>().Property(e => e.Username).IsRequired().HasMaxLength(30);
            builder.Entity<Guardian>().Property(e => e.Email).IsRequired().HasMaxLength(30);
            builder.Entity<Guardian>().Property(e => e.FirstName).IsRequired().HasMaxLength(60);
            builder.Entity<Guardian>().Property(e => e.LastName).IsRequired().HasMaxLength(60);
            builder.Entity<Guardian>().Property(e => e.Gender).IsRequired();
            builder.Entity<Guardian>().Property(e => e.Adress);

            builder.Entity<Guardian>().HasMany(e => e.Urgencies).WithOne(u => u.Guardian).HasForeignKey(u => u.GuardianId);

            //Urgency
            builder.Entity<Urgency>().ToTable("Urgencies");
            builder.Entity<Urgency>().HasKey(u => u.Id);
            builder.Entity<Urgency>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Urgency>().Property(u => u.Title).IsRequired();
            builder.Entity<Urgency>().Property(u => u.Summary);
            builder.Entity<Urgency>().Property(u => u.Latitude).IsRequired();
            builder.Entity<Urgency>().Property(u => u.Longitude).IsRequired();
            builder.Entity<Urgency>().Property(u => u.ReportedAt).IsRequired().ValueGeneratedOnAdd();

            //Apply Naming Convention
            builder.ApplySnakeCaseNamingConvention();
        }
    }
}
