using ApplicantTracking.Api.Configurations;
using ApplicantTracking.Infrastructure.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicantTrackingDb")));
builder.Services.AddDependencyInjection();
builder.Services.AddMediatorConfig();
builder.Services.AddApiConfig();
builder.Services.AddSwaggerConfig();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseSwaggerConfig();

app.UseApiConfig();
app.MapControllers();
app.Run();
