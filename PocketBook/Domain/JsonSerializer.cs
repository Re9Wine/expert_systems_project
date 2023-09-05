using System.Text.Json;

namespace Domain
{
    public static class JsonSerializer<T>
    {
        public static T? Deserialize(object data)
        {
            try
            {
                if(data is not JsonElement element)
                {
                    return default;
                }

                return JsonSerializer.Deserialize<T>(element);
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
