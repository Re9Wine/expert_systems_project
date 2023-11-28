using Microsoft.Extensions.DependencyInjection;
using PocketBook.BLL.Mappings.Profiles;
using PocketBook.BLL.Services.MoneyTransactionServices;
using PocketBook.BLL.Services.TransactionCategoryServices;

namespace PocketBook.BLL;

public static class DI
{
    public static void AddBLL(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(
            typeof(MoneyTransactionMapperConfiguration),
            typeof(TransactionCategoryMapperConfiguration));
        
        serviceCollection.AddScoped<ITransactionCategoryService, TransactionCategoryService>();
        serviceCollection.AddScoped<IMoneyTransactionService, MoneyTransactionService>();
    }
}