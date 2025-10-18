using System.ComponentModel.DataAnnotations;

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
        public int LecturerId { get; set; } //foriegn key

        public Lecturer Lecturer { get; set; } 

        [Required]
        public string Month { get; set; } 

        [Required]
        public int HoursWorked { get; set; }

        [Required]
        public decimal HourlyRate { get; set; }

        public decimal Amount => HoursWorked * HourlyRate; 

        [Required, StringLength(200)]
        public string Description { get; set; }

        [Required]
        public string SupportingDocument { get; set; } 

        public ClaimStatus Status { get; set; } = ClaimStatus.Submitted;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Feedback> FeedbackMessages { get; set; }
    }
}
