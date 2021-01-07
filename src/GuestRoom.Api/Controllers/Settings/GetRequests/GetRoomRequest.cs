using System.Threading;
using System.Threading.Tasks;
using MediatR;

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

    public class GetRoomResponse { }

    public class GetRoomRequestHandler : IRequestHandler<GetRoomRequest, GetRoomResponse>
    {
        public async Task<GetRoomResponse> Handle(GetRoomRequest request, CancellationToken cancellationToken)
        {
            return new();
        }
    }
}