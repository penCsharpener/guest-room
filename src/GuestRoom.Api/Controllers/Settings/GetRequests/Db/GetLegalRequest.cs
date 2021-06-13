using GuestRoom.Domain;
using GuestRoom.Domain.Models.Content;
using GuestRoom.Domain.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.GetRequests.Db
{
    public class GetLegalRequest : IRequest<GetLegalResponse> { }

    public class GetLegalResponse
    {
        public LegalModel Legal { get; set; }
    }

    public class GetLegalRequestHandler : IRequestHandler<GetLegalRequest, GetLegalResponse>
    {
        private readonly AppDbContext _context;
        private readonly IJsonConverter _jsonConverter;

        public GetLegalRequestHandler(AppDbContext context, IJsonConverter jsonConverter)
        {
            _context = context;
            _jsonConverter = jsonConverter;
        }

        public async Task<GetLegalResponse> Handle(GetLegalRequest request, CancellationToken cancellationToken)
        {
            var response = new GetLegalResponse();

            var entity = await _context.TextModels.SingleOrDefaultAsync(x => x.TextModelType == TextModelTypes.Legal);

            response.Legal = _jsonConverter.FromJsonAsync<LegalModel>(entity.JsonText);

            return response;
        }
    }
}
