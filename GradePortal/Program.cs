using GradeDO;
using GradePortal.Configurations;
using GradePortal.Controllers;
using GradePortal.Services;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Runtime;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<List<ExerciseService>>(builder.Configuration.GetSection("ExerciseService"));
builder.Services.Configure<Techer>(builder.Configuration.GetSection("Techer"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IGradeManager, GradeManager>();
builder.Services.AddTransient<IPasswordManager, PasswordManager>();
builder.Services.AddSingleton<IStudents, Students>();
builder.Services.AddTransient<IGradeManager,GradeManager>();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console() 
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) 
    .CreateLogger();

builder.Host.UseSerilog();


var app = builder.Build();
app.UseExceptionHandler("/error");
if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();
