using GuestRoom.Api.Extensions;
using GuestRoom.Domain.Models.Content;
using GuestRoom.Domain.Providers;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.UpdateCommands
{
    public class UpdateHomeCommand : HomeModel, IRequest<UpdateHomeResponse> { }

    public class UpdateHomeResponse { }

    public class UpdateHomeCommandHandler : IRequestHandler<UpdateHomeCommand, UpdateHomeResponse>
    {
        private readonly IContentStore _store;
        private readonly IWebHostEnvironment _env;

        public UpdateHomeCommandHandler(IContentStore store, IWebHostEnvironment env)
        {
            _store = store;
            _env = env;
        }

        public async Task<UpdateHomeResponse> Handle(UpdateHomeCommand request, CancellationToken cancellationToken)
        {
            var model = (HomeModel)request;

            await _store.WriteContentAsync(model, _env.GetAssetPath("site-content"), "home");

            return new();
        }
    }
}