
using ClientServiceApplication.Commands;
using ClientServiceApplication.Queries;
using ClientServiceData.Context;
using ClientServiceDomain.Repositories;
using ClientServiceDomain.Services;
using ClientServiceInfrastructure.Repositories;
using ClientServiceInfrastructure.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* Database context dependency injection */
//var dbHost = Environment.GetEnvironmentVariable("DB_HOST_CLIENT_SERVICE");
//var dbName = Environment.GetEnvironmentVariable("DB_NAME_CLIENT");
//var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD_CLIENT");
//var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};TrustServerCertificate=true;User ID=sa;Password={dbPassword}";
builder.Services.AddDbContext<ApplicationClientDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddHealthChecks()
    .AddCheck("ClientApiCheck", () => HealthCheckResult.Healthy())
    .AddDbContextCheck<ApplicationClientDbContext>(
        name: "ApplicationCarDbContext",
        failureStatus: HealthStatus.Unhealthy,
        tags: new[] { "ready", "db" }
    );
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(),
    typeof(CreateClientCommand).Assembly,
    typeof(GetAllClientsQuery).Assembly,
    typeof(GetClientByIdQuery).Assembly,
    typeof(UpdateClientCommand).Assembly,
    typeof(DeleteClientCommand).Assembly
    ));
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();

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
