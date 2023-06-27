using Microsoft.EntityFrameworkCore;
using SportsComplex.Models;

namespace SportsComplex
{
    public class SportsComplexDbContext : DbContext
    {
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Coach> Coachs { get; set; }
        public DbSet<Client> Clients { get; set; }

        public SportsComplexDbContext(DbContextOptions<SportsComplexDbContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Discipline>()
                .HasKey(d => d.Id);
            modelBuilder.Entity<Discipline>()
                .Property(d => d.Name)
                .HasMaxLength(40)
                .IsRequired();
            modelBuilder.Entity<Discipline>()
                .HasMany(d => d.Coaches)
                .WithOne(c => c.Discipline)
                .HasForeignKey(c => c.DisciplineId);

            modelBuilder.Entity<Coach>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Coach>()
                .Property(c => c.Name)
                .HasMaxLength(30)
                .IsRequired();
            modelBuilder.Entity<Coach>()
                .Property(c => c.Surname)
                .HasMaxLength(45)
                .IsRequired();
            modelBuilder.Entity<Coach>()
                .HasMany(c => c.Clients)
                .WithOne(cl => cl.Coach)
                .HasForeignKey(cl => cl.CoachId);

            modelBuilder.Entity<Client>()
                .HasKey(cl => cl.Id);
            modelBuilder.Entity<Client>()
                .Property(cl => cl.Name)
                .HasMaxLength(30)
                .IsRequired();
            modelBuilder.Entity<Client>()
                .Property(cl => cl.Surname)
                .HasMaxLength(45)
                .IsRequired();
            modelBuilder.Entity<Client>()
                .Property(cl => cl.DateOfBirth)
                .HasColumnType("date");
        }
    }
}
