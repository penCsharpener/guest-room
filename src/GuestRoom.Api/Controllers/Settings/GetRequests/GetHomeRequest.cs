using System.Threading;
using System.Threading.Tasks;
using GuestRoom.Domain.Models.Content;
using GuestRoom.Domain.Providers;
using MediatR;

namespace GuestRoom.Api.Controllers.Settings.GetRequests
{
    public class GetHomeRequest : IRequest<GetHomeResponse> { }

    public class GetHomeResponse
    {
        public HomeModel Home { get; set; }
    }

    public class GetHomeRequestHandler : IRequestHandler<GetHomeRequest, GetHomeResponse>
    {
        private readonly IContentStore _store;

        public GetHomeRequestHandler(IContentStore store)
        {
            _store = store;
        }

        public async Task<GetHomeResponse> Handle(GetHomeRequest request, CancellationToken cancellationToken)
        {
            var response = new GetHomeResponse();

            response.Home = await _store.GetContentAsync<HomeModel>("home");

            return response;
        }
    }
}