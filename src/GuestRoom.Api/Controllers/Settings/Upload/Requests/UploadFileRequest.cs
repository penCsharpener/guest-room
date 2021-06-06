using GuestRoom.Domain.Providers;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
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
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<UploadFileRequestHandler> _logger;
        private readonly string _basePath;

        public UploadFileRequestHandler(IFileProvider fileProvider, IWebHostEnvironment env, ILogger<UploadFileRequestHandler> logger)
        {
            _fileProvider = fileProvider;
            _env = env;
            _logger = logger;
            _basePath = _env.WebRootPath;
        }

        public async Task<UploadFileResponse> Handle(UploadFileRequest request, CancellationToken cancellationToken)
        {
            var response = new UploadFileResponse();

            await _fileProvider.WriteAllBytesAsync(request.FileContent, _basePath, $"assets\\{request.ImageName}");

            return response;
        }
    }
}