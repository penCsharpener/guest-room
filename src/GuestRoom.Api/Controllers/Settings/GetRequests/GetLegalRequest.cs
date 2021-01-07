using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace GuestRoom.Api.Controllers.Settings.GetRequests
{
    public class GetLegalRequest : IRequest<GetLegalResponse> { }

    public class GetLegalResponse { }

    public class GetLegalRequestHandler : IRequestHandler<GetLegalRequest, GetLegalResponse>
    {
        public async Task<GetLegalResponse> Handle(GetLegalRequest request, CancellationToken cancellationToken)
        {
            return new();
        }
    }
}