using GuestRoom.Domain.Providers;
using MediatR;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.Upload.Requests
{
    public class UploadFileRequest : IRequest<UploadFileResponse>
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public byte[] FileContent { get; set; }
    }

    public class UploadFileResponse { }

    public class UploadFileRequestHandler : IRequestHandler<UploadFileRequest, UploadFileResponse>
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<UploadFileRequestHandler> _logger;
        private readonly string _basePath;

        public UploadFileRequestHandler(IFileProvider fileProvider, ILogger<UploadFileRequestHandler> logger)
        {
            _fileProvider = fileProvider;
            _logger = logger;
            _basePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        }

        public async Task<UploadFileResponse> Handle(UploadFileRequest request, CancellationToken cancellationToken)
        {
            var response = new UploadFileResponse();

            await _fileProvider.WriteAllBytesAsync(request.FileContent, _basePath, $"Assets\\Images\\{request.ImageName}");

            return response;
        }
    }
}