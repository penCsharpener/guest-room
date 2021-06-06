using GuestRoom.Api.Extensions;
using GuestRoom.Domain.Models.Content;
using GuestRoom.Domain.Providers;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.Threading;
using System.Threading.Tasks;

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
        private readonly IWebHostEnvironment _env;

        public GetHomeRequestHandler(IContentStore store, IWebHostEnvironment env)
        {
            _store = store;
            _env = env;
        }

        public async Task<GetHomeResponse> Handle(GetHomeRequest request, CancellationToken cancellationToken)
        {
            var response = new GetHomeResponse();

            response.Home = await _store.GetContentAsync<HomeModel>(_env.GetAssetPath("site-content"), "home");

            return response;
        }
    }
}