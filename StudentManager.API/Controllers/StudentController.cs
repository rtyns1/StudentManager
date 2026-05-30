
using Microsoft.AspNetCore.Mvc; // imports th namespaces that contain [ApiController], ControllerBase, [HttpGet], Ok().
using Microsoft.EntityFrameworkCore;
using StudentManager.API.Data;
using StudentManager.Shared;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// this is a student controller that manages students, GET, POST and later PUT, DELETE.

namespace StudentManager.API.Controllers
{
    [ApiController] // -- An attribute, it tells ASP.NET Core that this class is meant to handle web Api requests. it enables automatic validation, binding and other stuff that we dont need to do.
    [Route("api/[controller]")] //Another attribute that defines the URL pattern. 
        /*
         * [controller] here is a placeholder, it will be replaced by the class name without the Controller, for example, StudentController becomes Student, so the route is api/Student.
         * 
         */



    public class StudentController : ControllerBase// inherititiln from this class ivees us inbuilt methods like Ok(), BadRequest(), NotFound(), etc etc
    {
        private readonly AppDbContext _appDbcontext;
        public StudentController (AppDbContext appDbcontext)
        {
            //constructor
            _appDbcontext = appDbcontext;
        }
        

        [HttpGet] //An attribute that marks the following method as the one to call when the HTTP method is GET
        public async Task<IActionResult> GetAll()// IAction is an INTERFACE that acts as a flexible containter for various HTTP responses     
        {
            /*
             *  IAction is an INTERFACE that acts as a flexible containter for various HTTP responses
             *  We use it when our action method might return different types of results for examples::
             *  When looking up user ID, i might want to return smoethnig like: Return 404 Not foubd- useer does not exist.
             *  or return 400Bad Request --if the ID is invalid, or return 200 OK with the user data if the call is succesful.
             *  All these responses have different data types and status codes, they all niherit and implement IActionResult to provide a unified return type.
             *  IN controllers, we dont build these errors frmo scratch, they come from the ControllerBase that we inherit, for example::
             *  \(200\)NotFound() -> Returns HTTP 
             *  \(404\)BadRequest() -> Returns HTTP 
             *  \(400\)Unauthorized() -> Returns HTTP 
             *  \(401\)RedirectToAction() -> Returns HTTP \(302\)
             *  u can use IActionResult or ActionResult, know what is the avnatage of using one instead of the other and in what scenarios.
             *  
             */

            var students = await _appDbcontext.Students.ToListAsync();
            return Ok(students);
        }
        [HttpPost]
        public async Task <IActionResult> Create([FromBody] Shared.Models.Student student)
        {
            
            if (string.IsNullOrWhiteSpace(student.Name))
                return BadRequest("Student name is required.");
            if (string.IsNullOrWhiteSpace(student.Email))
                return BadRequest("Student Email required.");
            student.CreatedAt = DateTime.UtcNow;
            _appDbcontext.Students.Add(student);
            await _appDbcontext.SaveChangesAsync();
            //return CreatedAtAction(nameof(GetAll), new { id = student.Id }, student);
            return Ok(student);

        }
        // for MVP, the only working things i need in a constroller to have a minimum working version are:
        /*
         * GET/api/students - list all students, i already have this
         * POST /api/students - add a new student, i already have this.
         * 
         * Then later i can add::
         * GET /api/student{id} - get a single student for editing
         * PUT /ai/students/{id{ to update a student
         * DELETE/api/students/{id} to deleta a student
         * 
         * There is still alot, ALOT to be done but we go step by step, as we see that what we build actually works.
         */

    }
}


