using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Alert
    {
        [Key]
        public long AlertID { get; set; }

        public int PatientID { get; set; }
        public long? DataID { get; set; }

        [Required]
        public string Message { get; set; } = default!;

        [Required]
        public string Severity { get; set; } = "High"; // Info, Warn, High

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool Acknowledged { get; set; }

        [ForeignKey("PatientID")]
        public User Patient { get; set; } = default!;

        [ForeignKey("DataID")]
        public SensorFrame? Data { get; set; }
    }
}
