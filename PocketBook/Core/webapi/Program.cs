using FluentMigrator.Runner;
using PocketBook.BLL;
using PocketBook.DAL;
using webapi.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilterAttribute>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDAL(connectionString);
builder.Services.AddBLL();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var runner = services.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();