using Dotnetstore.OvenSimulator.Contracts.Queries;
using Dotnetstore.OvenSimulator.Email.Models;
using Dotnetstore.OvenSimulator.Email.Queries;
using Dotnetstore.OvenSimulator.Email.Services;
using MediatR;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Dotnetstore.OvenSimulator.Email.Tests.Queries;

public class SendOvenErrorEmailQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCallSendEmailAsync()
    {
        // Arrange
        var emailService = Substitute.For<IEmailService>();
        var smtpSettings = Substitute.For<IOptions<Smtp>>();
        smtpSettings.Value.Returns(new Smtp
        {
            Host = "smtp.example.com",
            ErrorFrom = "error@example.com",
            ErrorSender = "Oven Simulator",
            NoReplyFrom = "noreply@example.com",
            ErrorTo = "admin@example.com",
            ErrorSubject = "Error Occurred"
        });
        var handler = new SendOvenErrorEmailQueryHandler(emailService, smtpSettings);
        var query = new SendOvenErrorEmailQuery("error message");
        var cancellationToken = CancellationToken.None;

        // Act
        await ((IRequestHandler<SendOvenErrorEmailQuery>)handler).Handle(query, cancellationToken);

        // Assert
        await emailService.Received(1).SendEmailAsync(
            "admin@example.com",
            "Error Occurred",
            "error message",
            cancellationToken);
    }
}