using GuestRoom.Domain.Models.Content;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.UpdateCommands
{
    public class UpdateContactCommand : ContactModel, IRequest<UpdateContactResponse> { }

    public class UpdateContactResponse { }

    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, UpdateContactResponse>
    {
        public async Task<UpdateContactResponse> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            return new();
        }
    }
}