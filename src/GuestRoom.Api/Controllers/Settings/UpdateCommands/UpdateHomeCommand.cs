using GuestRoom.Domain.Models.Content;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.UpdateCommands
{
    public class UpdateHomeCommand : HomeModel, IRequest<UpdateHomeResponse> { }

    public class UpdateHomeResponse { }

    public class UpdateHomeCommandHandler : IRequestHandler<UpdateHomeCommand, UpdateHomeResponse>
    {
        public async Task<UpdateHomeResponse> Handle(UpdateHomeCommand request, CancellationToken cancellationToken)
        {
            return new();
        }
    }
}