using System.Threading.Tasks;

namespace GuestRoom.Domain.Providers
{
    public interface IContentStore
    {
        Task<T> GetContentAsync<T>(string path, string fileName);
        Task WriteContentAsync<T>(T model, string path, string fileName);
    }
}