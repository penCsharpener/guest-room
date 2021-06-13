using GuestRoom.Domain;
using GuestRoom.Domain.Models.Content;
using GuestRoom.Domain.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.UpdateCommands.Db
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
        private readonly AppDbContext _context;
        private readonly IJsonConverter _jsonConverter;

        public UpdateRoomCommandHandler(AppDbContext context, IJsonConverter jsonConverter)
        {
            _context = context;
            _jsonConverter = jsonConverter;
        }

        public async Task<UpdateRoomResponse> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var modelType = request.Id switch
            {
                1 => TextModelTypes.Room1,
                2 => TextModelTypes.Room2
            };

            var entity = await _context.TextModels.SingleOrDefaultAsync(x => x.TextModelType == modelType);
            entity.JsonText = _jsonConverter.ToJsonAsync(request.RoomModel);

            await _context.SaveChangesAsync();

            return new();
        }
    }
}
