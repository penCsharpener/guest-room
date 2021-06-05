using System;
using System.IO;
using System.Text;
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
                return await File.ReadAllTextAsync(path, Encoding.UTF8, cancellationToken);
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

        public async Task<byte[]> ReadAllBytesAsync(string path, CancellationToken cancellationToken = default)
        {
            if (File.Exists(path))
            {
                return await File.ReadAllBytesAsync(path, cancellationToken);
            }

            return Array.Empty<byte>();
        }

        public async Task WriteAllBytesAsync(byte[] content, params string[] pathParts)
        {
            var path = Path.Combine(pathParts);

            CreateDirectoryIfNotExists(path);

            await File.WriteAllBytesAsync(path, content);
        }


        private void CreateDirectoryIfNotExists(string path)
        {
            var dir = Path.GetDirectoryName(path);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}