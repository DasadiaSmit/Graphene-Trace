using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GrapheneTrace.Models;

namespace GrapheneTrace.Models
{
    public class Alert
    {
        [Key]
        public long AlertID { get; set; }

        // ✅ Relation to User (Patient)
        public int PatientID { get; set; }

        // ✅ Relation to SensorFrame (optional)
        public long? DataID { get; set; }

        [Required]
        [StringLength(255)]
        public string Message { get; set; } = default!;

        [Required]
        [StringLength(50)]
        public string Severity { get; set; } = "High"; // Info, Warn, High

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool Acknowledged { get; set; }

        // ✅ Navigation properties
        [ForeignKey("PatientID")]
        public User? Patient { get; set; }

        [ForeignKey("DataID")]
        public SensorFrame? Data { get; set; }
    }
}
