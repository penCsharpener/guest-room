using GuestRoom.Api.Extensions;
using GuestRoom.Domain.Models.Content;
using GuestRoom.Domain.Providers;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.GetRequests
{
    public class GetContactRequest : IRequest<GetContactResponse> { }

    public class GetContactResponse
    {
        public ContactModel Contact { get; set; }
    }

    public class GetContactRequestHandler : IRequestHandler<GetContactRequest, GetContactResponse>
    {
        private readonly IContentStore _store;
        private readonly IWebHostEnvironment _env;

        public GetContactRequestHandler(IContentStore store, IWebHostEnvironment env)
        {
            _store = store;
            _env = env;
        }

        public async Task<GetContactResponse> Handle(GetContactRequest request, CancellationToken cancellationToken)
        {
            var response = new GetContactResponse();

            response.Contact = await _store.GetContentAsync<ContactModel>(_env.GetAssetPath("site-content"), "contact");

            return response;
        }
    }
}