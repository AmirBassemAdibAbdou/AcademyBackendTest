using Microsoft.EntityFrameworkCore;
using AcademyBackendTest.Api.Data;
using AcademyBackendTest.Api.Repositories;
using AcademyBackendTest.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register PostgreSQL DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    // Register Dependency Injection for Repositories and Services
    builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
    builder.Services.AddScoped<IPlaylistService, PlaylistService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();