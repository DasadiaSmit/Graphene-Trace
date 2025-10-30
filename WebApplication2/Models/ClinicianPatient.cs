using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class ClinicianPatient
    {
        public int ClinicianID { get; set; }
        public int PatientID { get; set; }
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        [ForeignKey("ClinicianID")]
        public User Clinician { get; set; } = default!;

        [ForeignKey("PatientID")]
        public User Patient { get; set; } = default!;
    }
}
