using GuestRoom.Domain;
using GuestRoom.Domain.Models.Content;
using GuestRoom.Domain.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;


namespace GuestRoom.Api.Controllers.Settings.GetRequests.Db
{
    public class GetContactRequest : IRequest<GetContactResponse> { }

    public class GetContactResponse
    {
        public ContactModel Contact { get; set; }
    }

    public class GetContactRequestHandler : IRequestHandler<GetContactRequest, GetContactResponse>
    {
        private readonly AppDbContext _context;
        private readonly IJsonConverter _jsonConverter;

        public GetContactRequestHandler(AppDbContext context, IJsonConverter jsonConverter)
        {
            _context = context;
            _jsonConverter = jsonConverter;
        }

        public async Task<GetContactResponse> Handle(GetContactRequest request, CancellationToken cancellationToken)
        {
            var response = new GetContactResponse();

            var entity = await _context.TextModels.SingleOrDefaultAsync(x => x.TextModelType == TextModelTypes.Contact);

            response.Contact = _jsonConverter.FromJsonAsync<ContactModel>(entity.JsonText);

            return response;
        }
    }
}
