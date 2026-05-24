using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentManager.API.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services for Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // 'UseSwaggerUI' is the correct method name, defined in the 'Microsoft.AspNetCore.Builder' namespace.
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

// This is a temporary in-memory database for testing; do not use in production.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();