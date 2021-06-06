using GuestRoom.Api.Extensions;
using GuestRoom.Domain.Models.Content;
using GuestRoom.Domain.Providers;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.UpdateCommands
{
    public class UpdateContactCommand : ContactModel, IRequest<UpdateContactResponse> { }

    public class UpdateContactResponse { }

    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, UpdateContactResponse>
    {
        private readonly IContentStore _store;
        private readonly IWebHostEnvironment _env;

        public UpdateContactCommandHandler(IContentStore store, IWebHostEnvironment env)
        {
            _store = store;
            _env = env;
        }

        public async Task<UpdateContactResponse> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var model = (ContactModel)request;

            await _store.WriteContentAsync(model, _env.GetAssetPath("site-content"), "contact");

            return new();
        }
    }
}