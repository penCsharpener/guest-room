using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Domain.Providers
{
    public class FileProvider : IFileProvider
    {
        public async Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default)
        {
            if (File.Exists(path))
            {
                return await File.ReadAllTextAsync(path, cancellationToken);
            }

            return "{ }";
        }

        public async Task<string> ReadAllTextAsync(params string[] pathParts)
        {
            return await ReadAllTextAsync(Path.Combine(pathParts));
        }

        public async Task WriteAllTextAsync(string content, params string[] pathParts)
        {
            var path = Path.Combine(pathParts);

            if (File.Exists(path))
            {
                await File.WriteAllTextAsync(content, path);
            }
        }
    }
}