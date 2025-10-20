using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LecturerClaimSystem.Models
{
    public enum ClaimStatus
    {
        Submitted,     // lecturer submits
        Rejected,      // rejected by coordinator or manager
        Forwarded,     // forwarded by coordinator
        Approved       // final approval by manager
    }

    public class Claim
    {
        public int ClaimId { get; set; }

        [Required]
        public int LecturerId { get; set; } // foreign key

        public Lecturer? Lecturer { get; set; }

        [Required, StringLength(50)]
        public string Month { get; set; }

        [Required]
        public int HoursWorked { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal HourlyRate { get; set; }

        // Computed property — do not map to DB
        [NotMapped]
        public decimal Amount => HoursWorked * HourlyRate;

        [Required, StringLength(200)]
        public string Description { get; set; }

        public string? SupportingDocument { get; set; }

        public ClaimStatus Status { get; set; } = ClaimStatus.Submitted;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // initialize collection to avoid null refs in views/controllers
        public ICollection<Feedback> FeedbackMessages { get; set; } = new List<Feedback>();
    }
}
