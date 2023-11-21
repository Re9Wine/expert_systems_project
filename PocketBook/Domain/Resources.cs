using Domain.DatabaseEntity;

namespace Domain;

public static class Resources
{
    public const string ExcessConsumptionByCategoryFormat =
        "По категории {0} вы потратили слишком много, нужно уменьшить расходы";

    public const string ExcessConsumption = "Вы потратили больше денег, чем имеете";

    public static readonly Dictionary<string, OperationCategory> StandardCategories = new()
    {
        {
            "Машины", new OperationCategory
            {
                Id = Guid.NewGuid(),
                Name = "Машины",
                Priority = 4,
                Limit = 0,
                IsChangeable = false,
                IsConsumption = true,
            }
        },
        {
            "Одежда", new OperationCategory
            {
                Id = Guid.NewGuid(),
                Name = "Одежда",
                Priority = 5,
                Limit = 0,
                IsChangeable = false,
                IsConsumption = true,
            }
        },
        {
            "Коммуникация", new OperationCategory
            {
                Id = Guid.NewGuid(),
                Name = "Коммуникация",
                Priority = 5,
                Limit = 0,
                IsChangeable = false,
                IsConsumption = true,
            }
        },
        {
            "Перекусы вне дома", new OperationCategory
            {
                Id = Guid.NewGuid(),
                Name = "Перекусы вне дома",
                Priority = 3,
                Limit = 0,
                IsChangeable = false,
                IsConsumption = true,
            }
        },
        {
            "Развлечения", new OperationCategory
            {
                Id = Guid.NewGuid(),
                Name = "Развлечения",
                Priority = 3,
                Limit = 0,
                IsChangeable = false,
                IsConsumption = true,
            }
        },
        {
            "Еда", new OperationCategory
            {
                Id = Guid.NewGuid(),
                Name = "Еда",
                Priority = 8,
                Limit = 0,
                IsChangeable = false,
                IsConsumption = true,
            }
        },
        {
            "Подарки", new OperationCategory
            {
                Id = Guid.NewGuid(),
                Name = "Подарки",
                Priority = 3,
                Limit = 0,
                IsChangeable = false,
                IsConsumption = true,
            }
        },
        {
            "Здоровье", new OperationCategory
            {
                Id = Guid.NewGuid(),
                Name = "Здоровье",
                Priority = 6,
                Limit = 0,
                IsChangeable = false,
                IsConsumption = true,
            }
        },
        {
            "Дом", new OperationCategory
            {
                Id = Guid.NewGuid(),
                Name = "Дом",
                Priority = 3,
                Limit = 0,
                IsChangeable = false,
                IsConsumption = true,
            }
        },
        {
            "Питомцы", new OperationCategory
            {
                Id = Guid.NewGuid(),
                Name = "Питомцы",
                Priority = 1,
                Limit = 0,
                IsChangeable = false,
                IsConsumption = true,
            }
        },
        {
            "Спорт", new OperationCategory
            {
                Id = Guid.NewGuid(),
                Name = "Спорт",
                Priority = 1,
                Limit = 0,
                IsChangeable = false,
                IsConsumption = true,
            }
        },
        {
            "Такси", new OperationCategory
            {
                Id = Guid.NewGuid(),
                Name = "Такси",
                Priority = 1,
                Limit = 0,
                IsChangeable = false,
                IsConsumption = true,
            }
        },
        {
            "Гигиена", new OperationCategory
            {
                Id = Guid.NewGuid(),
                Name = "Гигиена",
                Priority = 4,
                Limit = 0,
                IsChangeable = false,
                IsConsumption = true,
            }
        },
        {
            "Транспорт", new OperationCategory
            {
                Id = Guid.NewGuid(),
                Name = "Транспорт",
                Priority = 4,
                Limit = 0,
                IsChangeable = false,
                IsConsumption = true,
            }
        },
        {
            "Счета", new OperationCategory
            {
                Id = Guid.NewGuid(),
                Name = "Счета",
                Priority = 6,
                Limit = 0,
                IsChangeable = false,
                IsConsumption = true,
            }
        },
        {
            "Депозиты", new OperationCategory
            {
                Id = Guid.NewGuid(),
                Name = "Депозиты",
                Priority = 0,
                Limit = 0,
                IsChangeable = false,
                IsConsumption = false,
            }
        },
        {
            "Зарплата", new OperationCategory
            {
                Id = Guid.NewGuid(),
                Name = "Зарплата",
                Priority = 0,
                Limit = 0,
                IsChangeable = false,
                IsConsumption = false,
            }
        },
        {
            "Накопления", new OperationCategory
            {
                Id = Guid.NewGuid(),
                Name = "Накопления",
                Priority = 0,
                Limit = 0,
                IsChangeable = false,
                IsConsumption = false,
            }
        },
    };
}