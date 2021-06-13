using GuestRoom.Domain;
using GuestRoom.Domain.Models.Content;
using GuestRoom.Domain.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.GetRequests.Db
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
        private readonly AppDbContext _context;
        private readonly IJsonConverter _jsonConverter;

        public GetRoomRequestHandler(AppDbContext context, IJsonConverter jsonConverter)
        {
            _context = context;
            _jsonConverter = jsonConverter;
        }

        public async Task<GetRoomResponse> Handle(GetRoomRequest request, CancellationToken cancellationToken)
        {
            var response = new GetRoomResponse();

            var modelType = request.Id switch
            {
                1 => TextModelTypes.Room1,
                2 => TextModelTypes.Room2
            };

            var entity = await _context.TextModels.SingleOrDefaultAsync(x => x.TextModelType == modelType);

            response.Room = _jsonConverter.FromJsonAsync<RoomModel>(entity.JsonText);

            return response;
        }
    }
}
