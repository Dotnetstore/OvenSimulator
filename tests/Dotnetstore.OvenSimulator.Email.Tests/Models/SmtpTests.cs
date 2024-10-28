using Dotnetstore.OvenSimulator.Email.Models;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Dotnetstore.OvenSimulator.Email.Tests.Models;

public class SmtpTests
{
    [Fact]
    public void Smtp_ShouldSetAndGetProperties()
    {
        // Arrange
        var smtp = new Smtp
        {
            Host = "smtp.example.com",
            Port = 587,
            Username = "user@example.com",
            Password = "password",
            ErrorFrom = "error@example.com",
            ErrorSender = "Error Sender",
            ErrorTo = "admin@example.com",
            ErrorSubject = "Error Occurred",
            NoReplyFrom = "noreply@example.com"
        };

        // Assert
        using (new AssertionScope())
        {
            smtp.Host.Should().Be("smtp.example.com");
            smtp.Port.Should().Be(587);
            smtp.Username.Should().Be("user@example.com");
            smtp.Password.Should().Be("password");
            smtp.ErrorFrom.Should().Be("error@example.com");
            smtp.ErrorSender.Should().Be("Error Sender");
            smtp.ErrorTo.Should().Be("admin@example.com");
            smtp.ErrorSubject.Should().Be("Error Occurred");
            smtp.NoReplyFrom.Should().Be("noreply@example.com");
        }
    }

    [Fact]
    public void Smtp_ShouldRequireMandatoryProperties()
    {
        // Act
        var smtp = new Smtp
        {
            Host = "smtp.example.com",
            Port = 587,
            ErrorFrom = "error@example.com",
            ErrorSender = "Error Sender",
            ErrorTo = "admin@example.com",
            ErrorSubject = "Error Occurred",
            NoReplyFrom = "noreply@example.com"
        };

        // Assert
        using (new AssertionScope())
        {
            smtp.Host.Should().NotBeNull();
            smtp.ErrorFrom.Should().NotBeNull();
            smtp.ErrorSender.Should().NotBeNull();
            smtp.ErrorTo.Should().NotBeNull();
            smtp.ErrorSubject.Should().NotBeNull();
            smtp.NoReplyFrom.Should().NotBeNull();
        }
    }
}