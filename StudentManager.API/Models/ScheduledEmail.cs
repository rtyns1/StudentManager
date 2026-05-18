namespace StudentManager.API.Models
{
    public class ScheduledEmail
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? AttachmentPath { get; set; }
        public DateTime ScheduledAt { get; set; }
        public bool IsSent { get; set; }
        public DateTime? SentAt { get; set; } = DateTime.UtcNow;
    }
}
