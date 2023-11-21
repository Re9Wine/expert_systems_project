using DAL;
using DAL.Implementations;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Service.Implementations;
using Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IOperationCategoryRepository, OperationCategoryRepository>();
builder.Services.AddScoped<IOperationWithMoneyRepository, OperationWithMoneyRepository>();

builder.Services.AddScoped<IOperationCategoryService, OperationCategoryService>();
builder.Services.AddScoped<IOperationWithMoneyService, OperationWithMoneyService>();

builder.Services.AddControllers();
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

app.UseCors();

app.Run();
