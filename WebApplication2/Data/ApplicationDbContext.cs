using GrapheneTrace.Models;
using Microsoft.EntityFrameworkCore;
using GrapheneTrace.Models;

namespace GrapheneTrace.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ClinicianPatient> ClinicianPatients { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<SensorFrame> SensorFrames { get; set; }
        public DbSet<FrameMetric> FrameMetrics { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentReply> CommentReplies { get; set; }
        public DbSet<Alert> Alerts { get; set; }
    }
}
