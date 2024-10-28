using FluentEmail.Core;
using FluentEmail.Core.Models;
using Microsoft.Extensions.Logging;

namespace Dotnetstore.OvenSimulator.Email.Services;

internal sealed class EmailService(
    IFluentEmail fluentEmail,
    ILogger<EmailService> logger) : IEmailService
{
    async ValueTask<SendResponse> IEmailService.SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken)
    {
        var response = await fluentEmail
            .To(to)
            .Subject(subject)
            .Body(body)
            .SendAsync(cancellationToken);
        
        if (!response.Successful)
            logger.LogError(string.Join(Environment.NewLine, response.ErrorMessages));
        else
            logger.LogInformation("Email sent successfully with subject: {subject}, with message: {body}, to: {to}", subject, body, to);
        
        return response;
    }
}