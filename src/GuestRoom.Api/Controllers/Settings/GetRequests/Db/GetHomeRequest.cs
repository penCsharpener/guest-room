using GuestRoom.Domain;
using GuestRoom.Domain.Models.Content;
using GuestRoom.Domain.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.GetRequests.Db
{
    public class GetHomeRequest : IRequest<GetHomeResponse> { }

    public class GetHomeResponse
    {
        public HomeModel Home { get; set; }
    }

    public class GetHomeRequestHandler : IRequestHandler<GetHomeRequest, GetHomeResponse>
    {
        private readonly AppDbContext _context;
        private readonly IJsonConverter _jsonConverter;

        public GetHomeRequestHandler(AppDbContext context, IJsonConverter jsonConverter)
        {
            _context = context;
            _jsonConverter = jsonConverter;
        }

        public async Task<GetHomeResponse> Handle(GetHomeRequest request, CancellationToken cancellationToken)
        {
            var response = new GetHomeResponse();

            var entity = await _context.TextModels.SingleOrDefaultAsync(x => x.TextModelType == TextModelTypes.Home);

            response.Home = _jsonConverter.FromJsonAsync<HomeModel>(entity.JsonText);

            return response;
        }
    }
}
