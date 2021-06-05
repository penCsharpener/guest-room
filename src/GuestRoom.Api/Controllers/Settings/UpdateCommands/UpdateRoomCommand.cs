using GuestRoom.Domain.Models.Content;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.UpdateCommands
{
    public class UpdateRoomCommand : IRequest<UpdateRoomResponse>
    {
        public UpdateRoomCommand(int id, RoomModel roomModel)
        {
            Id = id;
            RoomModel = roomModel;
        }

        public int Id { get; }
        public RoomModel RoomModel { get; }
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