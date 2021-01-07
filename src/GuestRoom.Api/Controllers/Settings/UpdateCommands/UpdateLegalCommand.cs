using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace GuestRoom.Api.Controllers.Settings.UpdateCommands
{
    public class UpdateLegalCommand : IRequest<UpdateLegalResponse> { }

    public class UpdateLegalResponse { }

    public class UpdateLegalCommandHandler : IRequestHandler<UpdateLegalCommand, UpdateLegalResponse>
    {
        public async Task<UpdateLegalResponse> Handle(UpdateLegalCommand request, CancellationToken cancellationToken)
        {
            return new();
        }
    }
}