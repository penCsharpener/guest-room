using System.Threading.Tasks;

namespace GuestRoom.Domain.Providers
{
    public class ContentStore : IContentStore
    {
        private readonly IFileProvider _fileProvider;
        private readonly IJsonConverter _jsonConverter;

        public ContentStore(IFileProvider fileProvider, IJsonConverter jsonConverter)
        {
            _fileProvider = fileProvider;
            _jsonConverter = jsonConverter;
        }

        public async Task<T> GetContentAsync<T>(string path, string fileName)
        {
            var json = await _fileProvider.ReadAllTextAsync(path, fileName + ".json");

            return _jsonConverter.FromJsonAsync<T>(json);
        }

        public async Task WriteContentAsync<T>(T model, string path, string fileName)
        {
            var json = _jsonConverter.ToJsonAsync(model);

            await _fileProvider.WriteAllTextAsync(json, path, fileName + ".json");
        }
    }
}