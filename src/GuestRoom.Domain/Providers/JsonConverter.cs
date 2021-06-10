using System.Text.Json;

namespace GuestRoom.Domain.Providers
{
    public class JsonConverter : IJsonConverter
    {
        internal static JsonSerializerOptions _options = new() { AllowTrailingCommas = true, PropertyNameCaseInsensitive = false, PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true };

        public T FromJsonAsync<T>(string jsonText)
        {
            return JsonSerializer.Deserialize<T>(jsonText, _options);
        }

        public string ToJsonAsync<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, _options);
        }
    }
}