using Microsoft.EntityFrameworkCore;
using SparePartsServiceData.Context;
using SparePartsServiceDomain.Repositories;
using SparePartsServiceDomain.Services;
using SparePartsServiceInfrastructure.Repositories;
using SparePartsServiceInfrastructure.Services;
using System.Reflection;

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
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped<ISparePartRepository, SparePartRepository>();
builder.Services.AddScoped<ISparePartService, SparePartService>();

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
