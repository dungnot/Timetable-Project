using Microsoft.EntityFrameworkCore;
using ProjectScheduleManagement.Models;
using ProjectScheduleManagement.Service;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ScheduleManagementContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<FileUploadService, FileUploadService>();
builder.Services.AddScoped<ImportFileService, ImportFileService>();
builder.Services.AddScoped<ValidationService, ValidationService>();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();

