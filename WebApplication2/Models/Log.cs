using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Log
    {
        [Key]
        public long LogID { get; set; }

        public int? UserID { get; set; }

        [Required]
        public string Action { get; set; } = default!;

        public string? Details { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserID")]
        public User? User { get; set; }
    }
}
