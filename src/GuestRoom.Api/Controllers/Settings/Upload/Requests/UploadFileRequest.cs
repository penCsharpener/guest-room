using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<UploadFileRequestHandler> _logger;

        public UploadFileRequestHandler(ILogger<UploadFileRequestHandler> logger)
        {
            _logger = logger;
        }

        public async Task<UploadFileResponse> Handle(UploadFileRequest request, CancellationToken cancellationToken)
        {
            var response = new UploadFileResponse();

            return response;
        }
    }
}