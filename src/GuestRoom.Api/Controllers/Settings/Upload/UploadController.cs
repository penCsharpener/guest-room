using GuestRoom.Api.Controllers.Settings.Upload.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings.Upload
{
    [ApiController]
    [Authorize]
    [Route("api/settings/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UploadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult> Upload([FromForm] ImageUploadApiModel model)
        {
            var response = await _mediator.Send(new UploadFileRequest
            {
                Id = model.Id,
                File = model.File,
                Description = model.Description,
                Location = model.Location
            });

            return Ok();
        }
    }
}