using GuestRoom.Api.Controllers.Contact;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContactController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> SendMessageAsync([FromBody] SendMessageApiModel model)
    {
        var response = await _mediator.Send(new SendRequest()
        {
            Address = model.Address,
            Email = model.Email,
            MessageBody = model.MessageBody,
            Name = model.Name,
            Subject = model.Subject,
            Title = model.Title
        });

        return Ok(response);
    }
}
