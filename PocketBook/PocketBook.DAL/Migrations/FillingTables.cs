using FluentMigrator;
using PocketBook.Domain.Entities;

namespace PocketBook.DAL.Migrations;

[Migration(202311250002)]
public class FillingTables : Migration
{
    private readonly List<TransactionCategory> transactionCategories = new()
    {
        new TransactionCategory
        {
            Id = Guid.NewGuid(),
            Name = "Машины",
            Priority = 5,
            Limit = 0,
            IsChangeable = false,
            IsConsumption = true,
        },
        new TransactionCategory
        {
            Id = Guid.NewGuid(),
            Name = "Одежда",
            Priority = 6,
            Limit = 0,
            IsChangeable = false,
            IsConsumption = true,
        },
        new TransactionCategory
        {
            Id = Guid.NewGuid(),
            Name = "Коммуникация",
            Priority = 4,
            Limit = 0,
            IsChangeable = false,
            IsConsumption = true,
        },
        new TransactionCategory
        {
            Id = Guid.NewGuid(),
            Name = "Перекусы вне дома",
            Priority = 5,
            Limit = 0,
            IsChangeable = false,
            IsConsumption = true,
        },
        new TransactionCategory
        {
            Id = Guid.NewGuid(),
            Name = "Развлечения",
            Priority = 4,
            Limit = 0,
            IsChangeable = false,
            IsConsumption = true,
        },
        new TransactionCategory
        {
            Id = Guid.NewGuid(),
            Name = "Еда",
            Priority = 9,
            Limit = 0,
            IsChangeable = false,
            IsConsumption = true,
        },
        new TransactionCategory
        {
            Id = Guid.NewGuid(),
            Name = "Подарки",
            Priority = 3,
            Limit = 0,
            IsChangeable = false,
            IsConsumption = true,
        },
        new TransactionCategory
        {
            Id = Guid.NewGuid(),
            Name = "Здоровье",
            Priority = 10,
            Limit = 0,
            IsChangeable = false,
            IsConsumption = true,
        },
        new TransactionCategory
        {
            Id = Guid.NewGuid(),
            Name = "Дом",
            Priority = 10,
            Limit = 0,
            IsChangeable = false,
            IsConsumption = true,
        },
        new TransactionCategory
        {
            Id = Guid.NewGuid(),
            Name = "Питомцы",
            Priority = 7,
            Limit = 0,
            IsChangeable = false,
            IsConsumption = true,
        },
        new TransactionCategory
        {
            Id = Guid.NewGuid(),
            Name = "Спорт",
            Priority = 6,
            Limit = 0,
            IsChangeable = false,
            IsConsumption = true,
        },
        new TransactionCategory
        {
            Id = Guid.NewGuid(),
            Name = "Такси",
            Priority = 4,
            Limit = 0,
            IsChangeable = false,
            IsConsumption = true,
        },
        new TransactionCategory
        {
            Id = Guid.NewGuid(),
            Name = "Гигиена",
            Priority = 9,
            Limit = 0,
            IsChangeable = false,
            IsConsumption = true,
        },
        new TransactionCategory
        {
            Id = Guid.NewGuid(),
            Name = "Транспорт",
            Priority = 6,
            Limit = 0,
            IsChangeable = false,
            IsConsumption = true,
        },
        new TransactionCategory
        {
            Id = Guid.NewGuid(),
            Name = "Счета",
            Priority = 9,
            Limit = 0,
            IsChangeable = false,
            IsConsumption = true,
        },
        new TransactionCategory
        {
            Id = Guid.NewGuid(),
            Name = "Депозиты",
            Priority = 0,
            Limit = 0,
            IsChangeable = false,
            IsConsumption = false,
        },
        new TransactionCategory
        {
            Id = Guid.NewGuid(),
            Name = "Зарплата",
            Priority = 0,
            Limit = 0,
            IsChangeable = false,
            IsConsumption = false,
        },
        new TransactionCategory
        {
            Id = Guid.NewGuid(),
            Name = "Накопления",
            Priority = 0,
            Limit = 0,
            IsChangeable = false,
            IsConsumption = false,
        },
    };
    
    public override void Up()
    {
        foreach (var transactionCategory in transactionCategories)
        {
            Insert.IntoTable(nameof(TransactionCategory)).Row(new
            {
                transactionCategory.Id,
                transactionCategory.Name,
                transactionCategory.Priority,
                transactionCategory.Limit,
                transactionCategory.IsChangeable,
                transactionCategory.IsConsumption
            });
        }
    }

    public override void Down()
    {
        foreach (var transactionCategory in transactionCategories)
        {
            Delete.FromTable(nameof(TransactionCategory)).Row(transactionCategory);
        }
    }
}