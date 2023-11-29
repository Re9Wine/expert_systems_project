using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using PocketBook.DAL.DbContexts;
using PocketBook.DAL.Migrations;
using PocketBook.DAL.Repositories.MoneyTransactionRepositories;
using PocketBook.DAL.Repositories.TransactionCategoryRepositories;

namespace PocketBook.DAL;

public static class DI
{
    public static void AddDAL(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<PocketBookDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.UseLazyLoadingProxies();
        });

        serviceCollection.AddFluentMigratorCore()
            .ConfigureRunner(runner => runner
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(InitializeTables).Assembly).For.Migrations());

        serviceCollection.AddScoped<ITransactionCategoryRepository, TransactionCategoryRepository>();
        serviceCollection.AddScoped<IMoneyTransactionRepository, MoneyTransactionRepository>();
    }
}