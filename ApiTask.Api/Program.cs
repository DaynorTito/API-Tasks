using ApiTask.Api;
using ApiTask.Data;
using ApiTask.Data.Repositories;
using ApiTask.Domain.Ports;
using ApiTask.Api.Adapters;
using ApiTask.Api.Security;
using ApiTask.Data.Migration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var migrationScript = new MigrationScript();
migrationScript.Migrate();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("MySqlConection");

builder.Services.AddDbContext<TaskDbContext>(options =>
        options.UseSqlite(connectionString));

builder.Services.AddScoped<IUserPersistencePort, UserPersistanceAdapter>();
builder
    .Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, SecurityAuth>("BasicAuthentication", null);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
