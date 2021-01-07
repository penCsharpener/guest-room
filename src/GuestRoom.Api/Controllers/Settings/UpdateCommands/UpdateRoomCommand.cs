using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace GuestRoom.Api.Controllers.Settings.UpdateCommands
{
    public class UpdateRoomCommand : IRequest<UpdateRoomResponse>
    {
        public UpdateRoomCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class UpdateRoomResponse { }

    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, UpdateRoomResponse>
    {
        public async Task<UpdateRoomResponse> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            return new();
        }
    }
}