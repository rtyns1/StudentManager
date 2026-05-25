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

    public class LessonLogsController : ControllerBase
    {
        public readonly AppDbContext _appDbcontext;

        public LessonLogsController(AppDbContext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }

        [HttpGet]

        public async Task <IActionResult> GetAll()
        {
            var lessonlogs = await _appDbcontext.LessonLogs.ToListAsync();
            return Ok(lessonlogs);
        }
        [HttpPost]
        public async Task <IActionResult> Create([FromBody]Shared.Models.LessonLog lessonlog)
        {
            if (lessonlog == null)
                return BadRequest("Lessonlog Id is required.");
            _appDbcontext.LessonLogs.Add(lessonlog);
            await _appDbcontext.SaveChangesAsync();
            return Ok(lessonlog);
        }
    }
}
