using System;
using System.ComponentModel.DataAnnotations;

namespace GrapheneTrace.Models
{
    public class Metric
    {
        [Key]
        public long MetricID { get; set; }

        public long DataID { get; set; }

        public int PeakPressure { get; set; }

        public decimal ContactAreaPct { get; set; }

        public DateTime CalculatedAt { get; set; }
    }
}
