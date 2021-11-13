using GuestRoom.Api.Models;
using GuestRoom.Domain;
using GuestRoom.Domain.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.GetRequests.Db
{
    public class GetImagesRequest : IRequest<GetImagesResponse> { }

    public class GetImagesResponse
    {
        public List<ImageApiModel> Images { get; set; }
    }

    public class GetImagesRequestHandler : IRequestHandler<GetImagesRequest, GetImagesResponse>
    {
        private readonly AppDbContext _context;
        private readonly IJsonConverter _jsonConverter;

        public GetImagesRequestHandler(AppDbContext context, IJsonConverter jsonConverter)
        {
            _context = context;
            _jsonConverter = jsonConverter;
        }

        public async Task<GetImagesResponse> Handle(GetImagesRequest request, CancellationToken cancellationToken)
        {
            var response = new GetImagesResponse();

            var entity = await _context.Images.Where(x => x.IsActive)
                .Select(x => new ImageApiModel
                {
                    Path = x.Path,
                    Description = x.Description,
                    Location = x.Location,
                    Name = x.Name
                }).ToListAsync(cancellationToken);

            response.Images = entity;

            return response;
        }
    }
}
