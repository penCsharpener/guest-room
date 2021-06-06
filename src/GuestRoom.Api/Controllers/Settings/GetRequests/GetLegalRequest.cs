using GuestRoom.Api.Extensions;
using GuestRoom.Domain.Models.Content;
using GuestRoom.Domain.Providers;
using MediatR;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _env;

        public GetLegalRequestHandler(IContentStore store, IWebHostEnvironment env)
        {
            _store = store;
            _env = env;
        }

        public async Task<GetLegalResponse> Handle(GetLegalRequest request, CancellationToken cancellationToken)
        {
            var response = new GetLegalResponse();

            response.Legal = await _store.GetContentAsync<LegalModel>(_env.GetAssetPath("site-content"), "legal");
            response.Legal.Contact = await _store.GetContentAsync<ContactModel>(_env.GetAssetPath("site-content"), "contact");

            return response;
        }
    }
}