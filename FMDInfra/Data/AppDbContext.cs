using FMDInfra.Models;
using Microsoft.EntityFrameworkCore;

namespace FMDInfra.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Participant> Participants { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lecture>()
                .HasMany(p => p.Participants)
                .WithOne(p => p.Lecture)
                .HasForeignKey(p => p.LectureId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
