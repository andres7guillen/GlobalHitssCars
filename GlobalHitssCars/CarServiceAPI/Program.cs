using CarServiceData.Context;
using CarServiceDomain.Repositories;
using CarServiceDomain.Services;
using CarServiceInfrastructure.Repositories;
using CarServiceInfrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* Database context dependency injection */
var dbHost = Environment.GetEnvironmentVariable("DB_HOST_CAR_SERVICE");
var dbName = Environment.GetEnvironmentVariable("DB_NAME_CAR");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD_CAR");
var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};TrustServerCertificate=true;User ID=sa;Password={dbPassword}";
builder.Services.AddDbContext<ApplicationCarDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(connectionString)));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();

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
