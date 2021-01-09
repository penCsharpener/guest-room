using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Domain.Providers
{
    public interface IFileProvider
    {
        Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default);
        Task<string> ReadAllTextAsync(params string[] pathParts);
        Task WriteAllTextAsync(string content, params string[] pathParts);
    }
}