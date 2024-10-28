using Dotnetstore.OvenSimulator.Contracts.Queries;
using Dotnetstore.OvenSimulator.Email.Models;
using Dotnetstore.OvenSimulator.Email.Services;
using MediatR;
using Microsoft.Extensions.Options;

namespace Dotnetstore.OvenSimulator.Email.Queries;

internal sealed class SendOvenErrorEmailQueryHandler(
    IEmailService emailService,
    IOptions<Smtp> smtpSettings) : IRequestHandler<SendOvenErrorEmailQuery>
{
    async Task IRequestHandler<SendOvenErrorEmailQuery>.Handle(SendOvenErrorEmailQuery request, CancellationToken cancellationToken)
    {
        await emailService.SendEmailAsync(
            smtpSettings.Value.ErrorTo,
            smtpSettings.Value.ErrorSubject,
            request.ErrorMessage,
            cancellationToken);
    }
}