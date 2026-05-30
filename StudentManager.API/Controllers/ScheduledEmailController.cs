using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManager.API.Data;
using StudentManager.Shared;
using StudentManager.Shared.Models;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudentManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ScheduledEmailController : ControllerBase
    {
        private readonly AppDbContext _appDbcontext;

        public ScheduledEmailController(AppDbContext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }

        [HttpGet("test")]
        public IActionResult test() => Ok("It works");
        

        [HttpPost]
        public async Task<IActionResult> Create ([FromBody] Shared.Models.ScheduledEmail scheduledEmail)
        {
            if (scheduledEmail == null)
                return BadRequest("You must provide an email.");
            if (scheduledEmail.StudentId <= 0)
                return BadRequest("Valid studentId is required.");
            scheduledEmail.ScheduledAt = scheduledEmail.ScheduledAt.ToUniversalTime();
            if (scheduledEmail.ScheduledAt <= DateTime.UtcNow)
                return BadRequest("ScheduledAt must be a future date/Time (UTC)");

            scheduledEmail.IsSent = false;
            scheduledEmail.SentAt = null;

            _appDbcontext.ScheduledEmails.Add(scheduledEmail);
            await _appDbcontext.SaveChangesAsync();

            return Ok(scheduledEmail);
        }
    }
}
