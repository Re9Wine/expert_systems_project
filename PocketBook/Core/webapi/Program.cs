using DAL;
using DAL.Implementations;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Service.Implementations;
using Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(optionts => optionts.UseNpgsql(builder.Configuration.GetConnectionString("DataBase")));

builder.Services.AddScoped<IOperationCategoryRepository, OperationCategoryRepository>();
builder.Services.AddScoped<IOperationWithMoneyRepository, OperationWithMoneyRepository>();

builder.Services.AddScoped<IOperationCategorySercvice, OperationCategorySercvice>();
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
