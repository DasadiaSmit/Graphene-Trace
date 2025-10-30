using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class CommentReply
    {
        [Key]
        public long ReplyID { get; set; }

        public long CommentID { get; set; }
        public int ClinicianID { get; set; }

        [Required]
        public string Text { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("CommentID")]
        public Comment Comment { get; set; } = default!;

        [ForeignKey("ClinicianID")]
        public User Clinician { get; set; } = default!;
    }
}
