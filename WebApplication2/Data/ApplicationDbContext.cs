using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<ClinicianPatient> ClinicianPatients => Set<ClinicianPatient>();
        public DbSet<SensorFrame> SensorFrames => Set<SensorFrame>();
        public DbSet<FrameMetric> FrameMetrics => Set<FrameMetric>();
        public DbSet<Alert> Alerts => Set<Alert>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<CommentReply> CommentReplies => Set<CommentReply>();
        public DbSet<Log> Logs => Set<Log>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);

            // Users
            b.Entity<User>().HasIndex(u => u.Email).IsUnique();
            b.Entity<User>().Property(u => u.Role).HasMaxLength(20);

            // ClinicianPatient (composite key + two FKs back to Users)
            b.Entity<ClinicianPatient>().HasKey(cp => new { cp.ClinicianID, cp.PatientID });
            b.Entity<ClinicianPatient>()
                .HasOne(cp => cp.Clinician).WithMany(u => u.ClinicianLinks)
                .HasForeignKey(cp => cp.ClinicianID)
                .OnDelete(DeleteBehavior.Restrict);
            b.Entity<ClinicianPatient>()
                .HasOne(cp => cp.Patient).WithMany(u => u.PatientLinks)
                .HasForeignKey(cp => cp.PatientID)
                .OnDelete(DeleteBehavior.Restrict);

            // SensorFrames
            b.Entity<SensorFrame>().HasIndex(sf => new { sf.PatientID, sf.Timestamp });
            // 1:1 FrameMetric ↔ SensorFrame via DataID
            b.Entity<FrameMetric>().HasIndex(fm => fm.DataID).IsUnique();
            b.Entity<FrameMetric>()
                .HasOne(fm => fm.Data).WithOne(sf => sf.Metric)
                .HasForeignKey<FrameMetric>(fm => fm.DataID)
                .OnDelete(DeleteBehavior.Cascade);

            // Alerts: FK to Patient should NOT cascade (avoid multiple cascade paths)
            b.Entity<Alert>()
                .HasOne(a => a.Patient).WithMany()
                .HasForeignKey(a => a.PatientID)
                .OnDelete(DeleteBehavior.Restrict);      // 👈 changed from Cascade

            // Optional link to SensorFrame can be null; do not cascade delete frames from alerts
            b.Entity<Alert>()
                .HasOne(a => a.Data).WithMany()
                .HasForeignKey(a => a.DataID)
                .OnDelete(DeleteBehavior.SetNull);

            // Comments: also do NOT cascade from Patient to avoid cycles
            b.Entity<Comment>()
                .HasOne(c => c.Patient).WithMany()
                .HasForeignKey(c => c.PatientID)
                .OnDelete(DeleteBehavior.Restrict);      // 👈 changed from Cascade

            b.Entity<Comment>()
                .HasOne(c => c.Data).WithMany()
                .HasForeignKey(c => c.DataID)
                .OnDelete(DeleteBehavior.SetNull);

            // Replies
            b.Entity<CommentReply>()
                .HasOne(r => r.Comment).WithMany(c => c.Replies)
                .HasForeignKey(r => r.CommentID)
                .OnDelete(DeleteBehavior.Cascade);       // deleting a comment deletes its replies

            b.Entity<CommentReply>()
                .HasOne(r => r.Clinician).WithMany()
                .HasForeignKey(r => r.ClinicianID)
                .OnDelete(DeleteBehavior.Restrict);

            // Logs: keep logs even if user is deleted
            b.Entity<Log>()
                .HasOne(l => l.User).WithMany()
                .HasForeignKey(l => l.UserID)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
