using GuestRoom.Api.Extensions;
using GuestRoom.Domain.Models.Content;
using GuestRoom.Domain.Providers;
using MediatR;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IContentStore _store;
        private readonly IWebHostEnvironment _env;

        public UpdateRoomCommandHandler(IContentStore store, IWebHostEnvironment env)
        {
            _store = store;
            _env = env;
        }

        public async Task<UpdateRoomResponse> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            await _store.WriteContentAsync(request.RoomModel, _env.GetAssetPath("site-content"), $"room-{request.Id}");

            return new();
        }
    }
}