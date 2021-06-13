using GuestRoom.Domain;
using GuestRoom.Domain.Models.Content;
using GuestRoom.Domain.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.UpdateCommands.Db
{
    public class UpdateContactCommand : ContactModel, IRequest<UpdateContactResponse> { }

    public class UpdateContactResponse { }

    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, UpdateContactResponse>
    {
        private readonly AppDbContext _context;
        private readonly IJsonConverter _jsonConverter;

        public UpdateContactCommandHandler(AppDbContext context, IJsonConverter jsonConverter)
        {
            _context = context;
            _jsonConverter = jsonConverter;
        }

        public async Task<UpdateContactResponse> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var model = (ContactModel)request;

            var entity = await _context.TextModels.SingleOrDefaultAsync(x => x.TextModelType == TextModelTypes.Contact);
            entity.JsonText = _jsonConverter.ToJsonAsync(model);

            await _context.SaveChangesAsync();

            return new();
        }
    }
}
