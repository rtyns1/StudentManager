
using Microsoft.AspNetCore.Mvc; // imports th namespaces that contain [ApiController], ControllerBase, [HttpGet], Ok().
using Microsoft.EntityFrameworkCore;
using StudentManager.API.Data;
using StudentManager.Shared;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StudentManager.Shared.Enums;

namespace StudentManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class LessonController : ControllerBase
    {
        // 2 endpoints:: Get/api/lessons - returns all lessons (just the entities, no extra joints for MVP+
        // POST/api/lessons -- accepts a Lesson object, sets Status = LessonStatus.Scheduled, saves , returns the created lesson

        private readonly AppDbContext _appDbcontext;
        public LessonController(AppDbContext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }
        [HttpGet ]
        public async Task<IActionResult> GetAll()
        {
            var lessons = await _appDbcontext.Lessons.ToListAsync();
            return Ok(lessons);

        }
        [HttpPost]
        public async Task <IActionResult> Create([FromBody] Shared.Models.Lesson lesson)
        {
            // how would we identify a lesson? not by name, but by ID
            if (lesson == null)
                return BadRequest("Lesson Id is required.");
            if (lesson.StudentId <= 0 || lesson.SubjectId <= 0)
                return BadRequest("StudentId and SubjectId are required.");
            lesson.Status = LessonStatusEnum.Scheduled;
            _appDbcontext.Lessons.Add(lesson);
            await _appDbcontext.SaveChangesAsync();
            return Ok(lesson);
        }
        /*
         * Specific API controller that handles HTTP requests for scheduled lessons
         * GET /api/lessons-- frontend calls this to display all scheduled lessons e.g on a calender
         * POST /api/lessons - frontend calls this when a new lesson is scheduled.
         * It is a backend to frontend bridge for lesson scheduling data. The controlle rtrnalsates HTTP requests into database operations on the Lessons table.
         * 
         * 
         */
    }
}
