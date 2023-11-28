using FluentMigrator;
using PocketBook.Domain.Entities;

namespace PocketBook.DAL.Migrations;

[Migration(202311250001)]
public class InitializeTables : AutoReversingMigration //TODO глянуть про ограничения
{
    public override void Up()
    {
        Create.Table(nameof(TransactionCategory))
            .WithColumn(nameof(TransactionCategory.Id)).AsGuid().NotNullable().WithDefaultValue(Guid.NewGuid())
                .PrimaryKey()
            .WithColumn(nameof(TransactionCategory.Name)).AsString(100).NotNullable()
            .WithColumn(nameof(TransactionCategory.Priority)).AsInt16().NotNullable()
            .WithColumn(nameof(TransactionCategory.Limit)).AsDecimal(12, 2).NotNullable()
            .WithColumn(nameof(TransactionCategory.IsChangeable)).AsBoolean().NotNullable()
            .WithColumn(nameof(TransactionCategory.IsConsumption)).AsBoolean().NotNullable();

        Create.Table(nameof(MoneyTransaction))
            .WithColumn(nameof(MoneyTransaction.Id)).AsGuid().NotNullable().WithDefaultValue(Guid.NewGuid())
                .PrimaryKey()
            .WithColumn(nameof(MoneyTransaction.CategoryId)).AsGuid().NotNullable()
                .ForeignKey(nameof(TransactionCategory), nameof(TransactionCategory.Id))
            .WithColumn(nameof(MoneyTransaction.Description)).AsString(100).NotNullable()
            .WithColumn(nameof(MoneyTransaction.Value)).AsDecimal(10, 2).NotNullable()
            .WithColumn(nameof(MoneyTransaction.Date)).AsDateTime().NotNullable();
    }
}