using System.Text.Json;

namespace Domain;

public static class JsonSerializer<T>
{
    public static T? Deserialize(object data)
    {
        try
        {
            return data is not JsonElement element ? default : element.Deserialize<T>();
        }
        catch
        {
            return default;
        }
    }
}