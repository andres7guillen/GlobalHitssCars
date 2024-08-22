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
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;
using System.Text.Json;

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
builder.Services.AddDbContext<ApplicationCarStockDbContext>(options =>
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

builder.Services.AddHealthChecks()
    .AddCheck("CarApiCheck", () => HealthCheckResult.Healthy())
    .AddDbContextCheck<ApplicationCarStockDbContext>(
        name: "ApplicationCarStockDbContext",
        failureStatus: HealthStatus.Unhealthy,
        tags: new[] { "ready", "db" }
    );


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(),
    typeof(CreateCarStockCommand).Assembly,
    typeof(GetAllCarsQuery).Assembly,
    typeof(GetCarByIdQuery).Assembly,
    typeof(GetCarByFilterQuery).Assembly,
    typeof(UpdateCarStockCommand).Assembly,
    typeof(DeleteCarStockCommand).Assembly
    ));
builder.Services.AddScoped<ICarStockRepository, CarStockRepository>();
builder.Services.AddScoped<ICarStockService, CarStockService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(entry => new {
                name = entry.Key,
                status = entry.Value.Status.ToString(),
                exception = entry.Value.Exception?.Message,
                duration = entry.Value.Duration.ToString()
            })
        });
        await context.Response.WriteAsync(result);
    }
});

app.MapControllers();

app.Run();
