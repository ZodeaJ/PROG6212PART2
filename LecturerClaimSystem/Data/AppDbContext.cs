using Microsoft.EntityFrameworkCore;
using LecturerClaimSystem.Models;

namespace LecturerClaimSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

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
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Claim>()
                .HasMany(c => c.FeedbackMessages)
                .WithOne(f => f.Claim)
                .HasForeignKey(f => f.ClaimId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Claim>()
                .Property(c => c.HourlyRate)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Claim>()
                .Property(c => c.Description)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Claim>()
                .Property(c => c.SupportingDocument)
                .IsRequired();

            modelBuilder.Entity<Claim>()
                .Property(c => c.Status)
                .HasConversion<string>()
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
