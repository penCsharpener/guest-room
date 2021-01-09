namespace GuestRoom.Domain.Providers
{
    public interface IJsonConverter
    {
        T FromJsonAsync<T>(string jsonText);
        string ToJsonAsync<T>(T obj);
    }
}