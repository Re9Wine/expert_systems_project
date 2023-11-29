using FluentMigrator;
using PocketBook.Domain.Entities;

namespace PocketBook.DAL.Migrations;

[Migration(202311250001)]
public class InitializeTables : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table(nameof(TransactionCategory))
            .WithColumn(nameof(TransactionCategory.Id)).AsGuid().NotNullable().WithDefaultValue(Guid.NewGuid())
                .PrimaryKey()
            .WithColumn(nameof(TransactionCategory.Name)).AsString(100).NotNullable()
            .WithColumn(nameof(TransactionCategory.Priority)).AsInt32().NotNullable()
            .WithColumn(nameof(TransactionCategory.Limit)).AsDecimal(12, 2).NotNullable()
            .WithColumn(nameof(TransactionCategory.IsChangeable)).AsBoolean().NotNullable()
            .WithColumn(nameof(TransactionCategory.IsConsumption)).AsBoolean().NotNullable();

        Create.Table(nameof(MoneyTransaction))
            .WithColumn(nameof(MoneyTransaction.Id)).AsGuid().NotNullable().WithDefaultValue(Guid.NewGuid())
                .PrimaryKey()
            .WithColumn(nameof(MoneyTransaction.TransactionCategoryId)).AsGuid().NotNullable()
            .WithColumn(nameof(MoneyTransaction.Description)).AsString(100).NotNullable()
            .WithColumn(nameof(MoneyTransaction.Value)).AsDecimal(10, 2).NotNullable()
            .WithColumn(nameof(MoneyTransaction.Date)).AsDateTime2().NotNullable();

        Create.ForeignKey()
            .FromTable(nameof(MoneyTransaction)).ForeignColumn(nameof(MoneyTransaction.TransactionCategoryId))
            .ToTable(nameof(TransactionCategory)).PrimaryColumn(nameof(TransactionCategory.Id));
    }
}