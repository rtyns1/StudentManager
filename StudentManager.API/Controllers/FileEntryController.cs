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

    public class FileEntryController : ControllerBase
    {
        private readonly AppDbContext _appDbcontext;

        public FileEntryController (AppDbContext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }
        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            var fileEntrylog = await _appDbcontext.FileEntryLogs.ToListAsync();
            return Ok(fileEntrylog);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Shared.Models.FileEntry fileEntrylog)
        {
            if (fileEntrylog == null)
                return BadRequest("FileEntryLog I is required.");
            _appDbcontext.FileEntryLogs.Add(fileEntrylog);
            await _appDbcontext.SaveChangesAsync();
            return Ok(fileEntrylog);
        }
    }
}
