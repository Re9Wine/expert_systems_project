namespace PocketBook.Domain.Resources;

public static class ValidationExceptionMessages
{
    public const string UpdatingObjectIsNotSelected = "Обновляемый объект не выбран";
    public const string FieldIsRequired = "Поле обязательно для заполнения";
    public const string OutOfStringMaxLenghtFormat = "Максимальная длинна строки - {1}";

    public const string OutOfDecimalPositiveValueFormal =
        "Число дожно быть положитнльным и не превышать значения - {2}";

    public const string CantDeserialize = "Ошибка десереализации";
    public const string DeserializeResultIsNull = "Ошибка десереализации. Объект = null";
    public const string StringIsEmpty = "Строка пуста";
}