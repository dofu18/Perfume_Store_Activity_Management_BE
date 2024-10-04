using Microsoft.EntityFrameworkCore;
using PerfumeStore.Repository;
using PerfumeStore.Repository.Models;
using PerfumeStore.Service.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database
//var connectionString = builder.Configuration.GetConnectionString("DefaulConnection");

//builder.Services.AddDbContext<PerfumeStoreActivityManagementContext>(options =>
//options.UseSqlServer(connectionString));

builder.Services.AddDbContext<PerfumeStoreActivityManagementContext>(options =>
{
    Console.WriteLine($"Using ConnectionString: {builder.Configuration.GetConnectionString("DatabaseConnection")}");
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
});

//Service
builder.Services.AddScoped<PerfumeService>();
builder.Services.AddScoped<UnitOfWork>();

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
