using GuestRoom.Domain.Models.Content;
using GuestRoom.Domain.Providers;
using MediatR;
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

        public GetRoomRequestHandler(IContentStore store)
        {
            _store = store;
        }

        public async Task<GetRoomResponse> Handle(GetRoomRequest request, CancellationToken cancellationToken)
        {
            var response = new GetRoomResponse();

            response.Room = await _store.GetContentAsync<RoomModel>($"room-{request.Id}");
            response.Room.Miscellaneous = await _store.GetContentAsync<MiscellaneousModel>("misc");

            return response;
        }
    }
}