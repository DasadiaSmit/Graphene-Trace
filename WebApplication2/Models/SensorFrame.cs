using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrapheneTrace.Models
{
    public class SensorFrame
    {
        [Key]
        public long DataID { get; set; }

        public int PatientID { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public string PayloadCsv { get; set; } = default!;

        public string? SourceFile { get; set; }

        // Navigation
        [ForeignKey("PatientID")]
        public User Patient { get; set; } = default!;

        public FrameMetric? Metric { get; set; }
    }
}
