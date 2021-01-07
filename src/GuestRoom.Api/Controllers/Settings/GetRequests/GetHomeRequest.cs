using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace GuestRoom.Api.Controllers.Settings.GetRequests
{
    public class GetHomeRequest : IRequest<GetHomeResponse> { }

    public class GetHomeResponse { }

    public class GetHomeRequestHandler : IRequestHandler<GetHomeRequest, GetHomeResponse>
    {
        public async Task<GetHomeResponse> Handle(GetHomeRequest request, CancellationToken cancellationToken)
        {
            return new();
        }
    }
}