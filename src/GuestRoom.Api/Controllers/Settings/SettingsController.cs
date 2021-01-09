using System.Threading.Tasks;
using GuestRoom.Api.Controllers.Settings.GetRequests;
using GuestRoom.Api.Controllers.Settings.UpdateCommands;
using GuestRoom.Domain.Models.Content;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ContactModel> GetContact()
        {
            var response = await _mediator.Send(new GetContactRequest());

            return response.Contact;
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
        public async Task<ActionResult> UpdateContact()
        {
            var response = await _mediator.Send(new UpdateContactCommand());

            return Ok();
        }

        [HttpPut("home")]
        public async Task<ActionResult> UpdateHome()
        {
            var response = await _mediator.Send(new UpdateHomeCommand());

            return Ok();
        }

        [HttpPut("legal")]
        public async Task<ActionResult> UpdateLegal()
        {
            var response = await _mediator.Send(new UpdateLegalCommand());

            return Ok();
        }

        [HttpPut("room/{id:int}")]
        public async Task<ActionResult> UpdateRoom(int id)
        {
            var response = await _mediator.Send(new UpdateRoomCommand(id));

            return Ok();
        }
    }
}