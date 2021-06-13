using GuestRoom.Domain;
using GuestRoom.Domain.Models;
using GuestRoom.Domain.Providers;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.Upload.Requests
{
    public class UploadFileRequest : IRequest<UploadFileResponse>
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }

    public class UploadFileResponse { }

    public class UploadFileRequestHandler : IRequestHandler<UploadFileRequest, UploadFileResponse>
    {
        private readonly AppDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<UploadFileRequestHandler> _logger;

        public UploadFileRequestHandler(AppDbContext context, IFileProvider fileProvider, IWebHostEnvironment env, ILogger<UploadFileRequestHandler> logger)
        {
            _context = context;
            _fileProvider = fileProvider;
            _env = env;
            _logger = logger;
        }

        public async Task<UploadFileResponse> Handle(UploadFileRequest request, CancellationToken cancellationToken)
        {
            var response = new UploadFileResponse();

            var newImageName = $"{Guid.NewGuid()}_{Path.GetFileNameWithoutExtension(request.File.FileName)}{Path.GetExtension(request.File.FileName)}";

            var imageEntity = new Image
            {
                UploadedOn = DateTime.Now,
                Path = $"{_env.WebRootPath}\\assets\\images\\{newImageName}",
                Location = "",
                Name = newImageName,
                Description = "",
            };

            _context.Images.Add(imageEntity);
            await _context.SaveChangesAsync();

            await _fileProvider.WriteAllBytesAsync(await GetRawContent(request.File), imageEntity.Path);

            return response;
        }

        private async Task<byte[]> GetRawContent(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}