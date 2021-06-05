using GuestRoom.Api.Controllers.Settings.GetRequests;
using GuestRoom.Api.Controllers.Settings.UpdateCommands;
using GuestRoom.Domain.Models.Content;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Settings
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("contact")]
        public async Task<ActionResult<ContactModel>> GetContact()
        {
            var response = await _mediator.Send(new GetContactRequest());

            return Ok(response.Contact);
        }

        [HttpGet("home")]
        public async Task<ActionResult> GetHome()
        {
            var response = await _mediator.Send(new GetHomeRequest());

            return Ok();
        }

        [HttpGet("legal")]
        public async Task<ActionResult> GetLegal()
        {
            var response = await _mediator.Send(new GetLegalRequest());

            return Ok();
        }

        [HttpGet("room/{id:int}")]
        public async Task<ActionResult> GetRoom(int id)
        {
            var response = await _mediator.Send(new GetRoomRequest(id));

            return Ok();
        }

        [HttpPut("contact")]
        public async Task<ActionResult> UpdateContact([FromBody] UpdateContactCommand model)
        {
            var response = await _mediator.Send(model);

            return Ok();
        }

        [HttpPut("home")]
        public async Task<ActionResult> UpdateHome([FromBody] UpdateHomeCommand model)
        {
            var response = await _mediator.Send(model);

            return Ok();
        }

        [HttpPut("legal")]
        public async Task<ActionResult> UpdateLegal([FromBody] UpdateLegalCommand model)
        {
            var response = await _mediator.Send(model);

            return Ok();
        }

        [HttpPut("room/{id:int}")]
        public async Task<ActionResult> UpdateRoom([FromRoute] int id, [FromBody] RoomModel model)
        {
            var response = await _mediator.Send(new UpdateRoomCommand(id, model));

            return Ok();
        }
    }
}