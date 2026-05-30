using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentManager.API.Data;
using StudentManager.API.Services;


// next we need to create the builder, this is th eobject that will help us configure our application and build the web host.
var builder = WebApplication.CreateBuilder(args);
// step 3, register the AppDbContext--> this is how we tell our application about the AppDbContext class and how to create instances of it when needed.
/*How do we create this this register? we use the AppDbContext class and the AddDbContext method. We also need to provide the connection string for out database, this is done using the options parameter of the AddDbContext method.
 * The connection string is a string that contains all the information needed to connect to the database, such as the server name, database name, user id and password.In this case we are using SQL Server LocalDb, which is a lightweight version of SQL server that is ideal for development and testing.
 * 
 * 
 */
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers(); // this line adds the controllers to the services collection, this is necessary for the application to be able to find and use the controllers we created.
// registers controllers and other services in the dependency injection container, so that they can be injected into other parts of the application when needed. This is a crucial step for the application to function properly, as it allows us to use the controllers we created to handle Http requests and perform database operations.

//---> registering swagger services
//we do this in the following 2 lines::
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire(config => config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<EmailJob>();

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHangfireDashboard(); // optional: /hangfire to monitor jobs

app.UseHttpsRedirection();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();

    
}
app.Run();