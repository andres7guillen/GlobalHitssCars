using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PurchaseApplication.Commands;
using PurchaseApplication.Queries;
using PurchaseServiceData.Context;
using PurchaseServiceDomain.Repository;
using PurchaseServiceDomain.Services;
using PurchaseServiceInfrastructure.Repositories;
using PurchaseServiceInfrastructure.Services;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
/* Database context dependency injection */
//var dbHost = Environment.GetEnvironmentVariable("DB_HOST_PURCHASE_SERVICE");
//var dbName = Environment.GetEnvironmentVariable("DB_NAME_PURCHASE");
//var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD_PURCHASE");
//var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};TrustServerCertificate=true;User ID=sa;Password={dbPassword}";
//builder.Services.AddDbContext<ApplicationPurchaseDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(connectionString)));
builder.Services.AddDbContext<ApplicationPurchaseDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});
builder.Services.AddHealthChecks()
    .AddCheck("PurchaseApiCheck", () => HealthCheckResult.Healthy())
    .AddDbContextCheck<ApplicationPurchaseDbContext>(
        name: "ApplicationCarDbContext",
        failureStatus: HealthStatus.Unhealthy,
        tags: new[] { "ready", "db" }
    );
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(),
    typeof(GetAllPurchasesQuery).Assembly,
    typeof(GetPurchaseByIdQuery).Assembly,
    typeof(CreatePurchaseCommand).Assembly,
    typeof(DeletePurchaseCommand).Assembly,
    typeof(UpdatePurchaseCommand).Assembly
    ));
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();

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
