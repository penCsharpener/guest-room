using GuestRoom.Domain.Models.Content;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.UpdateCommands
{
    public class UpdateLegalCommand : LegalModel, IRequest<UpdateLegalResponse> { }

    public class UpdateLegalResponse { }

    public class UpdateLegalCommandHandler : IRequestHandler<UpdateLegalCommand, UpdateLegalResponse>
    {
        public async Task<UpdateLegalResponse> Handle(UpdateLegalCommand request, CancellationToken cancellationToken)
        {
            return new();
        }
    }
}