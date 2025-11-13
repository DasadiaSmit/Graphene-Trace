using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GrapheneTrace.Models;

namespace GrapheneTrace.Models
{
    public class Comment
    {
        [Key]
        public long CommentID { get; set; }

        // ✅ Foreign keys
        public int PatientID { get; set; }
        public long? DataID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ✅ Navigation properties
        [ForeignKey("PatientID")]
        public User? Patient { get; set; }

        [ForeignKey("DataID")]
        public SensorFrame? Data { get; set; }

        // ✅ Collection navigation for replies
        public ICollection<CommentReply> Replies { get; set; } = new List<CommentReply>();
    }
}
