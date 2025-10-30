using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Comment
    {
        [Key]
        public long CommentID { get; set; }

        public int PatientID { get; set; }
        public long? DataID { get; set; }

        [Required]
        public string Text { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("PatientID")]
        public User Patient { get; set; } = default!;

        [ForeignKey("DataID")]
        public SensorFrame? Data { get; set; }

        public ICollection<CommentReply> Replies { get; set; } = new List<CommentReply>();
    }
}
