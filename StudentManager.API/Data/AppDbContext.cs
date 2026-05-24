using Microsoft.EntityFrameworkCore;
using System.Text;
using System;
using StudentManager.Shared.Models;


namespace StudentManager.API.Data
{
    public class AppDbContext : DbContext
    {
        // this class the bridge btwn the models- data containers and te database.
        public AppDbContext(DbContextOptions<AppDbContext>options): base(options)
        {
            /*
             * Now there is a bit of explanatinos to do here for the appDbContext.
             * This is its constructor
             * This class niherits frmo DbContext, DbContext is a built in class provided by Entity FrameworkCore.
             * Now, why is the constructor syntax so wierd looking?
             * (DbContextOptions<AppDbContext> options) is the parameter of the constructor. It is an object that contans all the configuration details for DbContext.
             * This is where my conection string and other settings will be provided at runtime.
             * The : base(options)  -- base is a keyword that means we are calling the constructor of the parent class
             */

        }
        public DbSet<Student> Students{ get; set; }
        public DbSet<Subject> subjects { get; set; }
        /* this tells EF Core that:
         * Tells EF core that the structure of the Table should matsh your model class.
         * You can now use _context.Students in my code to quesry and save student records.
         */
        
    }
}
