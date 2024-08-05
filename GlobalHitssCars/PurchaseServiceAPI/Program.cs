using Microsoft.EntityFrameworkCore;
using PurchaseServiceData.Context;
using PurchaseServiceDomain.Repository;
using PurchaseServiceDomain.Services;
using PurchaseServiceInfrastructure.Repositories;
using PurchaseServiceInfrastructure.Services;
using System.Reflection;

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
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
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

app.MapControllers();

app.Run();
