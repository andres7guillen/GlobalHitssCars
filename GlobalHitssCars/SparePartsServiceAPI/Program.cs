using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SparePartServiceApplication.Commands;
using SparePartServiceApplication.Queries;
using SparePartsServiceData.Context;
using SparePartsServiceDomain.Repositories;
using SparePartsServiceDomain.Services;
using SparePartsServiceInfrastructure.Repositories;
using SparePartsServiceInfrastructure.Services;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/* Database context dependency injection */
//var dbHost = Environment.GetEnvironmentVariable("DB_HOST_SPARE_PARTS_SERVICE");
//var dbName = Environment.GetEnvironmentVariable("DB_NAME_SPARE_PARTS");
//var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD_SPARE_PARTS");
//var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};TrustServerCertificate=true;User ID=sa;Password={dbPassword}";
//builder.Services.AddDbContext<ApplicationSparePartDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(connectionString)));
builder.Services.AddDbContext<ApplicationSparePartDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});
builder.Services.AddHealthChecks()
    .AddCheck("SparePartApiCheck", () => HealthCheckResult.Healthy())
    .AddDbContextCheck<ApplicationSparePartDbContext>(
        name: "ApplicationCarDbContext",
        failureStatus: HealthStatus.Unhealthy,
        tags: new[] { "ready", "db" }
    );
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(),
    typeof(CreateSparePartCommand).Assembly,
    typeof(UpdateSparePartCommand).Assembly,
    typeof(LessStockSparePartCommand).Assembly,
    typeof(DeleteSparePartCommand).Assembly,
    typeof(GetAllSparePartsQuery).Assembly,
    typeof(GetSparePartsByFilterQuery).Assembly,
    typeof(GetSparePartByIdQuery).Assembly

    ));
builder.Services.AddScoped<ISparePartRepository, SparePartRepository>();
builder.Services.AddScoped<ISparePartService, SparePartService>();
//builder.Services.AddScoped<IMessageProducer, MessageProducer>();

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
            checks = report.Entries.Select(entry => new
            {
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
