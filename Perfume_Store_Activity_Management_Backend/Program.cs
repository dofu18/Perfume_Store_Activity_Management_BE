using Microsoft.EntityFrameworkCore;
using Perfume_Store_Activity_Management_Backend.src.Application.Services;
using Perfume_Store_Activity_Management_Backend.src.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
      builder =>
      {
          builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
      });
    options.AddDefaultPolicy(
      builder =>
      {
          builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
      });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DB Connect
builder.Services.AddDbContext<PerfumeStoreDbContext>(options =>
{
    Console.WriteLine($"Using ConnectionString: {builder.Configuration.GetConnectionString("DatabaseConnection")}");
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
});

//Auto Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Services
builder.Services.AddScoped<IPerfumeService, PerfumeService>();

builder.Services.AddControllers();
builder.Services.AddLogging();


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
