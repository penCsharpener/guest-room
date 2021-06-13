using GuestRoom.Domain;
using GuestRoom.Domain.Models.Content;
using GuestRoom.Domain.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.UpdateCommands.Db
{
    public class UpdateHomeCommand : HomeModel, IRequest<UpdateHomeResponse> { }

    public class UpdateHomeResponse { }

    public class UpdateHomeCommandHandler : IRequestHandler<UpdateHomeCommand, UpdateHomeResponse>
    {
        private readonly AppDbContext _context;
        private readonly IJsonConverter _jsonConverter;

        public UpdateHomeCommandHandler(AppDbContext context, IJsonConverter jsonConverter)
        {
            _context = context;
            _jsonConverter = jsonConverter;
        }

        public async Task<UpdateHomeResponse> Handle(UpdateHomeCommand request, CancellationToken cancellationToken)
        {
            var model = (HomeModel)request;

            var entity = await _context.TextModels.SingleOrDefaultAsync(x => x.TextModelType == TextModelTypes.Home);
            entity.JsonText = _jsonConverter.ToJsonAsync(model);

            await _context.SaveChangesAsync();

            return new();
        }
    }
}
