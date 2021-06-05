using GuestRoom.Domain.Models.Content;
using GuestRoom.Domain.Providers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.GetRequests
{
    public class GetLegalRequest : IRequest<GetLegalResponse> { }

    public class GetLegalResponse
    {
        public LegalModel Legal { get; set; }
    }

    public class GetLegalRequestHandler : IRequestHandler<GetLegalRequest, GetLegalResponse>
    {
        private readonly IContentStore _store;

        public GetLegalRequestHandler(IContentStore store)
        {
            _store = store;
        }

        public async Task<GetLegalResponse> Handle(GetLegalRequest request, CancellationToken cancellationToken)
        {
            var response = new GetLegalResponse();

            response.Legal = await _store.GetContentAsync<LegalModel>("legal");

            return response;
        }
    }
}