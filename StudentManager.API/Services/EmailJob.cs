using Microsoft.EntityFrameworkCore;
using StudentManager.API.Data;
using StudentManager.Shared.Models;
using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace StudentManager.API.Services
{
    public class EmailJob
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;

        public EmailJob(AppDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task SendPendingEmailsAsync()
        {
            var now = DateTime.UtcNow;
            var pendingEmails = await _context.ScheduledEmails
                .Where(e => !e.IsSent && e.ScheduledAt <= now)
                .ToListAsync();

            foreach (var email in pendingEmails)
            {
                var student = await _context.Students.FindAsync(email.StudentId);
                if (student == null) continue;

                string? attachmentPath = null;
                if (email.AttachmentFileId > 0)  
                {
                    var fileEntry = await _context.FileEntryLogs.FindAsync(email.AttachmentFileId); // <-- no .Value
                    if (fileEntry != null && !string.IsNullOrEmpty(fileEntry.StoredFileName))
                    {
                        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileEntry.StoredFileName);
                        if (File.Exists(fullPath))
                            attachmentPath = fullPath;
                    }
                }

                await _emailService.SendEmailAsync(student.Email, email.Subject, email.Body, attachmentPath);

                email.IsSent = true;
                email.SentAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }
    }
}