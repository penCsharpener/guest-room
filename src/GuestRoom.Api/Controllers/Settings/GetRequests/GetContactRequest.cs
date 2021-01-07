using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace GuestRoom.Api.Controllers.Settings.GetRequests
{
    public class GetContactRequest : IRequest<GetContactResponse> { }

    public class GetContactResponse { }

    public class GetContactRequestHandler : IRequestHandler<GetContactRequest, GetContactResponse>
    {
        public async Task<GetContactResponse> Handle(GetContactRequest request, CancellationToken cancellationToken)
        {
            return new();
        }
    }
}