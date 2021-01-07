using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace GuestRoom.Api.Controllers.Settings.UpdateCommands
{
    public class UpdateHomeCommand : IRequest<UpdateHomeResponse> { }

    public class UpdateHomeResponse { }

    public class UpdateHomeCommandHandler : IRequestHandler<UpdateHomeCommand, UpdateHomeResponse>
    {
        public async Task<UpdateHomeResponse> Handle(UpdateHomeCommand request, CancellationToken cancellationToken)
        {
            return new();
        }
    }
}