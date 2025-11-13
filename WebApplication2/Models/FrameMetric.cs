using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrapheneTrace.Models
{
    public class FrameMetric
    {
        [Key]
        public long MetricID { get; set; }

        public long DataID { get; set; }

        [Required]
        public int PeakPressure { get; set; }

        [Required]
        public decimal ContactAreaPct { get; set; }

        public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("DataID")]
        public SensorFrame Data { get; set; } = default!;
    }
}
