using movieSolution.Controllers;
using movieSolution.Models;
using movieSolution.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiKeyDBSettings>(builder.Configuration.GetSection("ApiKeyDB"));
builder.Services.AddSingleton<ApiKeyDBService>();

builder.Services.Configure<MovieDBSettings>(builder.Configuration.GetSection("MovieDB"));
builder.Services.AddSingleton<MovieDBService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
