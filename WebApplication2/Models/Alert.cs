using System;

namespace GrapheneTrace.Models
{
    public class Alert
    {
        public int AlertId { get; set; }
        public double Value { get; set; }
        public string? Message { get; set; }
        public string AlertType { get; set; } = "System";   // System / Emergency
        public DateTime Timestamp { get; set; }
    }
}
