using GuestRoom.Api.Extensions;
using GuestRoom.Domain.Models.Content;
using GuestRoom.Domain.Providers;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.GetRequests
{
    public class GetRoomRequest : IRequest<GetRoomResponse>
    {
        public GetRoomRequest(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class GetRoomResponse
    {
        public RoomModel Room { get; set; }
    }

    public class GetRoomRequestHandler : IRequestHandler<GetRoomRequest, GetRoomResponse>
    {
        private readonly IContentStore _store;
        private readonly IWebHostEnvironment _env;

        public GetRoomRequestHandler(IContentStore store, IWebHostEnvironment env)
        {
            _store = store;
            _env = env;
        }

        public async Task<GetRoomResponse> Handle(GetRoomRequest request, CancellationToken cancellationToken)
        {
            var response = new GetRoomResponse();

            response.Room = await _store.GetContentAsync<RoomModel>(_env.GetAssetPath("site-content"), $"room-{request.Id}");
            response.Room.Miscellaneous = await _store.GetContentAsync<MiscellaneousModel>(_env.GetAssetPath("site-content"), "misc");

            return response;
        }
    }
}