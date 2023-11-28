using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using PocketBook.Domain.Resources;

namespace webapi.Extensions;

public class JsonSerializer<T>
{
    public static T Deserialize(object data)
    {
        if (data is not JsonElement element)
        {
            throw new ValidationException(ValidationExceptionMessages.CantDeserialize);
        }

        var result = element.Deserialize<T>();

        if (result is null)
        {
            throw new ValidationException(ValidationExceptionMessages.DeserializeResultIsNull);
        }

        return result;

    }
}