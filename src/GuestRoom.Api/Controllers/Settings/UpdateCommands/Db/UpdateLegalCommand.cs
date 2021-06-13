using GuestRoom.Domain;
using GuestRoom.Domain.Models.Content;
using GuestRoom.Domain.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.UpdateCommands.Db
{
    public class UpdateLegalCommand : LegalModel, IRequest<UpdateLegalResponse> { }

    public class UpdateLegalResponse { }

    public class UpdateLegalCommandHandler : IRequestHandler<UpdateLegalCommand, UpdateLegalResponse>
    {
        private readonly AppDbContext _context;
        private readonly IJsonConverter _jsonConverter;

        public UpdateLegalCommandHandler(AppDbContext context, IJsonConverter jsonConverter)
        {
            _context = context;
            _jsonConverter = jsonConverter;
        }

        public async Task<UpdateLegalResponse> Handle(UpdateLegalCommand request, CancellationToken cancellationToken)
        {
            var model = (LegalModel)request;

            var entity = await _context.TextModels.SingleOrDefaultAsync(x => x.TextModelType == TextModelTypes.Legal);
            entity.JsonText = _jsonConverter.ToJsonAsync(model);

            await _context.SaveChangesAsync();

            return new();
        }
    }
}
