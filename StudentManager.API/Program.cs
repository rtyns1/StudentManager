using Microsoft.EntityFrameworkCore;
using StudentManager.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();




using (var scope = app.Services.CreateScope())
{
    
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
     dbContext.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.Run();

