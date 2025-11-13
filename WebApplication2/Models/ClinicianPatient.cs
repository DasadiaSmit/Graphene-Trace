using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrapheneTrace.Models
{
    public class ClinicianPatient
    {
        // ✅ Composite relationship between Clinician and Patient
        public int ClinicianID { get; set; }
        public int PatientID { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        // ✅ Navigation Properties
        [ForeignKey("ClinicianID")]
        public User? Clinician { get; set; }

        [ForeignKey("PatientID")]
        public User? Patient { get; set; }
    }
}
