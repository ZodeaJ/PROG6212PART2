namespace LecturerClaimSystem.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public int ClaimId { get; set; } //Foreign key
        public string Role { get; set; } //Coordinator, Manager
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
