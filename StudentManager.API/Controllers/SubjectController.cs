using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManager.API.Data;
using StudentManager.Shared.Models;


namespace StudentManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        /*
         * GET /api/subjects – return all subjects from _context.Subjects.
         * POST /api/subjects – accept [FromBody] Subject subject, validate Name is not empty, add to _context.Subjects, save, return CreatedAtAction.
         * Think about do i need to set CreatedAt for a subject?
         * If i want, adda  property DateTime CreatedAt property to the subject model and set it to DateTimeUTCNow in the POST method.
         * 
         * 
         */
        private readonly AppDbContext _appDbcontext;
        public SubjectController (AppDbContext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subjects = await _appDbcontext.subjects.ToListAsync();
            return Ok(subjects);
        }

        [HttpPost]
        public async Task <IActionResult> Create([FromBody] Shared.Models.Subject subject)
        {
            if (string.IsNullOrWhiteSpace(subject.Name))
                return BadRequest("Subject name is required.");
            _appDbcontext.subjects.Add(subject);
            await _appDbcontext.SaveChangesAsync();
            //return CreatedAtAction(nameof(GetAll), new { id = subject.Id }, subject);
            return Ok(subject);

            
        }

    }
}
