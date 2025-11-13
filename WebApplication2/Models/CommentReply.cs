using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrapheneTrace.Models
{
    public class CommentReply
    {
        [Key]
        public long ReplyID { get; set; }

        // ✅ Foreign Keys
        public long CommentID { get; set; }
        public int ClinicianID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ✅ Navigation Properties
        [ForeignKey("CommentID")]
        public Comment? Comment { get; set; }

        [ForeignKey("ClinicianID")]
        public User? Clinician { get; set; }
    }
}
