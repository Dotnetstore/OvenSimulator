using Dotnetstore.OvenSimulator.Contracts.Queries;
using Dotnetstore.OvenSimulator.Email.Models;
using FluentEmail.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Dotnetstore.OvenSimulator.Email.Queries;

internal sealed class SendOvenErrorEmailQueryHandler(
    IFluentEmail fluentEmail,
    IOptions<Smtp> smtpSettings,
    ILogger<SendOvenErrorEmailQueryHandler> logger) : IRequestHandler<SendOvenErrorEmailQuery>
{
    async Task IRequestHandler<SendOvenErrorEmailQuery>.Handle(SendOvenErrorEmailQuery request, CancellationToken cancellationToken)
    {
        var response = await fluentEmail
            .To(smtpSettings.Value.ErrorTo)
            .Subject(smtpSettings.Value.ErrorSubject)
            .Body(request.ErrorMessage)
            .SendAsync(cancellationToken);
        
        if (!response.Successful)
            logger.LogError(string.Join(Environment.NewLine, response.ErrorMessages));
        else
            logger.LogInformation("Error email sent successfully with message: {ErrorMessage}", request.ErrorMessage);
    }
}