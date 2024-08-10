using CarServiceApplication.Commands;
using CarServiceApplication.Queries;
using CarServiceData.Context;
using CarServiceDomain.Repositories;
using CarServiceDomain.Services;
using CarServiceInfrastructure.Repositories;
using CarServiceInfrastructure.Services;
using Common.Logging;
using Common.Logging.Implementations;
using Common.Logging.Interfaces;
using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configurar log4net
Log4NetConfig.Configure();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<ILog>(LogManager.GetLogger(typeof(Program)));
// Registrar el servicio de logging
builder.Services.AddSingleton<Common.Logging.Interfaces.ILogger>(provider =>
    new Log4NetLogger(typeof(Program)));
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationCarDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"),
    sqlServerOptionsAction: sqlOptions =>
    {
        //Resiliency config
        sqlOptions.
            EnableRetryOnFailure(maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    });
    options.ConfigureWarnings(warnings => warnings.Throw(
            RelationalEventId.QueryPossibleUnintendedUseOfEqualsWarning));
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(),
    typeof(CreateCarCommand).Assembly,
    typeof(GetAllCarsQuery).Assembly,
    typeof(GetCarByIdQuery).Assembly,
    typeof(GetCarByFilterQuery).Assembly,
    typeof(UpdateCarCommand).Assembly,
    typeof(DeleteCarCommand).Assembly
    ));
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
