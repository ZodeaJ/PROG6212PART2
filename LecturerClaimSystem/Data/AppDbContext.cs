using Microsoft.EntityFrameworkCore;
using LecturerClaimSystem.Models;

namespace LecturerClaimSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Coordinator> Coordinators { get; set; }
        public DbSet<Manager> Managers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lecturer>()
                .HasMany(l => l.Claims)
                .WithOne(c => c.Lecturer)
                .HasForeignKey(c => c.LecturerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Claim>()
                .HasMany(c => c.FeedbackMessages)
                .WithOne()
                .HasForeignKey(f => f.ClaimId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}