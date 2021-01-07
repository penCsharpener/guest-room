using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace GuestRoom.Api.Controllers.Settings.UpdateCommands
{
    public class UpdateContactCommand : IRequest<UpdateContactResponse> { }

    public class UpdateContactResponse { }

    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, UpdateContactResponse>
    {
        public async Task<UpdateContactResponse> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            return new();
        }
    }
}