using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace GuestRoom.Domain.Providers
{
    public class ContentStore : IContentStore
    {
        private readonly string _basePath;
        private readonly IFileProvider _fileProvider;
        private readonly IJsonConverter _jsonConverter;

        public ContentStore(IFileProvider fileProvider, IJsonConverter jsonConverter)
        {
            _fileProvider = fileProvider;
            _jsonConverter = jsonConverter;
            _basePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Assets");
        }

        public async Task<T> GetContentAsync<T>(string fileName)
        {
            var json = await _fileProvider.ReadAllTextAsync(_basePath, fileName + ".json");

            return _jsonConverter.FromJsonAsync<T>(json);
        }

        public async Task WriteContentAsync<T>(T model, string fileName)
        {
            var json = _jsonConverter.ToJsonAsync(model);

            await _fileProvider.WriteAllTextAsync(json, _basePath, fileName + ".json");
        }
    }
}