using GuestRoom.Api.Models.Configuration;
using MediatR;
using NETCore.MailKit.Core;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Contact;

public class SendRequest : IRequest<SendResponse>
{
    public string Title { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Subject { get; set; }
    public string MessageBody { get; set; }
}

public class SendResponse { }

public class SendRequestHandler : IRequestHandler<SendRequest, SendResponse>
{
    private readonly IEmailService _emailService;
    private readonly AppSettings _appSettings;

    public SendRequestHandler(IEmailService emailService, AppSettings appSettings)
    {
        _emailService = emailService;
        _appSettings = appSettings;
    }

    public async Task<SendResponse> Handle(SendRequest request, CancellationToken cancellationToken)
    {
        var response = new SendResponse();

        await _emailService.SendAsync(_appSettings.ContactOptions.RecipientAddress, request.Subject, request.MessageBody);

        return response;
    }
}
