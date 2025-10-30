using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; } = default!;

        [Required, EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public string PasswordHash { get; set; } = default!;

        [Required, MaxLength(20)]
        public string Role { get; set; } = default!; // Admin, Clinician, Patient

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<ClinicianPatient> ClinicianLinks { get; set; } = new List<ClinicianPatient>();
        public ICollection<ClinicianPatient> PatientLinks { get; set; } = new List<ClinicianPatient>();
    }
}
