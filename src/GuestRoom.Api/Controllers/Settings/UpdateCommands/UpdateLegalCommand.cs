using GuestRoom.Api.Extensions;
using GuestRoom.Domain.Models.Content;
using GuestRoom.Domain.Providers;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.UpdateCommands
{
    public class UpdateLegalCommand : LegalModel, IRequest<UpdateLegalResponse> { }

    public class UpdateLegalResponse { }

    public class UpdateLegalCommandHandler : IRequestHandler<UpdateLegalCommand, UpdateLegalResponse>
    {
        private readonly IContentStore _store;
        private readonly IWebHostEnvironment _env;


        public UpdateLegalCommandHandler(IContentStore store, IWebHostEnvironment env)
        {
            _store = store;
            _env = env;
        }

        public async Task<UpdateLegalResponse> Handle(UpdateLegalCommand request, CancellationToken cancellationToken)
        {
            var model = (LegalModel)request;

            await _store.WriteContentAsync(model, _env.GetAssetPath("site-content"), "legal");

            return new();
        }
    }
}