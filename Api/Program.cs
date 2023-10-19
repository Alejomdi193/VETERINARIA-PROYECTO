using Microsoft.EntityFrameworkCore;
using Persistencia;
using Api.Extensions;
using System.Reflection;
using AspNetCoreRateLimit;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureRateLimiting();
builder.Services.ConfigureApiVersioning();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.ConfigureCors();
builder.Services.AddAplicationServices();
builder.Services.AddAuthentication();
builder.Services.AddDbContext<VeterinariaContext>(options => 
    {
        string  ConnectionStrings= builder.Configuration.GetConnectionString("ConexMySql");
        options.UseMySql(ConnectionStrings , ServerVersion.AutoDetect(ConnectionStrings));
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseIpRateLimiting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
