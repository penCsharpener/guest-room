using GuestRoom.Api.Controllers.Contact;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Visitor;

[ApiController]
[Route("api/[controller]")]
public class VisitorController : ControllerBase
{
    private readonly IMediator _mediator;

    public VisitorController(IMediator mediator)
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

    [HttpPost]
    [Route("counter/{sessionId:guid}")]
    public async Task<ActionResult> CountAsync(Guid sessionId)
    {
        var count = await _mediator.Send(new CountVisitorRequest() { SessionId = sessionId });

        return Ok(count);
    }
}
