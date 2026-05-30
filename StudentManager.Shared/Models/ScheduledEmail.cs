namespace StudentManager.Shared.Models
{
    public class ScheduledEmail
    {
        public int ScheduledEmailId { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public int? AttachmentFileId { get; set; } // File attached -- links to file entry
        public DateTime ScheduledAt { get; set; }
        public bool IsSent { get; set; }
        public DateTime? SentAt { get; set; } = DateTime.UtcNow;
    }
}
