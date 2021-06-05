using GuestRoom.Domain.Models.Content;
using GuestRoom.Domain.Providers;
using MediatR;
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

        public GetContactRequestHandler(IContentStore store)
        {
            _store = store;
        }

        public async Task<GetContactResponse> Handle(GetContactRequest request, CancellationToken cancellationToken)
        {
            var response = new GetContactResponse();

            response.Contact = await _store.GetContentAsync<ContactModel>("contact");

            return response;
        }
    }
}