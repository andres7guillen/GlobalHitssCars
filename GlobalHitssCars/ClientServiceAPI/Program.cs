
using ClientServiceData.Context;
using ClientServiceDomain.Repositories;
using ClientServiceDomain.Services;
using ClientServiceInfrastructure.Repositories;
using ClientServiceInfrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
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

app.MapControllers();

app.Run();
