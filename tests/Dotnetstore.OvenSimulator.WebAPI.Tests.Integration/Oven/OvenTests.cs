using Dotnetstore.OvenSimulator.Oven.AddError;
using Dotnetstore.OvenSimulator.Oven.GetOvenStatus;
using Dotnetstore.OvenSimulator.SDK;
using Dotnetstore.OvenSimulator.SDK.Oven;
using Dotnetstore.OvenSimulator.SDK.Oven.Requests;
using Dotnetstore.OvenSimulator.SDK.Oven.Responses;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Dotnetstore.OvenSimulator.WebAPI.Tests.Integration.Oven;

public class OvenTests(DotnetstoreOvenSimulatorBase simulatorBase) : TestBase<DotnetstoreOvenSimulatorBase>
{
    [Fact, Priority(1)]
    public async Task LoadRecipe_ShouldReturnOk()
    {
        // Arrange
        var adminClient = simulatorBase.CreateClient(c =>
        {
            c.DefaultRequestHeaders.Authorization = new("Bearer", simulatorBase.ApiKey);
        });
        var url = ApiEndpoints.Oven.LoadRecipe.Replace("{name}", "Pizza");
        
        // Act
        var rsp = await adminClient.GetAsync(url);
        
        var (res, resp) = await adminClient.GETAsync<GetOvenStatusEndpoint, GetOvenStatusResponse>();
        
        // Assert
        using (new AssertionScope())
        {
            rsp.Should().BeSuccessful();
            resp.ActiveRecipe.Should().Be("Pizza");
        }
    }

    [Fact, Priority(2)]
    public async Task StartOven_ShouldReturnOk()
    {
        // Arrange
        var adminClient = simulatorBase.CreateClient(c =>
        {
            c.DefaultRequestHeaders.Authorization = new("Bearer", simulatorBase.ApiKey);
        });
        var url = ApiEndpoints.Oven.Start;
        
        // Act
        var rsp = await adminClient.PostAsync(url, null);
        
        var (res, resp) = await adminClient.GETAsync<GetOvenStatusEndpoint, GetOvenStatusResponse>();
        
        // Assert
        using (new AssertionScope())
        {
            rsp.Should().BeSuccessful();
            resp.HeatingElementOn.Should().BeTrue();
        }
    }

    [Fact, Priority(3)]
    public async Task AddError_ShouldReturnOk()
    {
        // Arrange
        var adminClient = simulatorBase.CreateClient(c =>
        {
            c.DefaultRequestHeaders.Authorization = new("Bearer", simulatorBase.ApiKey);
        });

        // Act
        var rsp = await adminClient.POSTAsync<AddErrorEndpoint, AddErrorRequest>(new AddErrorRequest
        {
            ErrorType = OvenErrorType.HeaterFailure
        });
        
        // Assert
        rsp.Should().BeSuccessful();
    }

    [Fact, Priority(4)]
    public async Task StopOven_ShouldReturnOk()
    {
        // Arrange
        var adminClient = simulatorBase.CreateClient(c =>
        {
            c.DefaultRequestHeaders.Authorization = new("Bearer", simulatorBase.ApiKey);
        });
        var url = ApiEndpoints.Oven.Stop;

        // Act
        var rsp = await adminClient.PostAsync(url, null);

        var (res, resp) = await adminClient.GETAsync<GetOvenStatusEndpoint, GetOvenStatusResponse>();

        // Assert
        using (new AssertionScope())
        {
            rsp.Should().BeSuccessful();
            resp.HeatingElementOn.Should().BeFalse();
        }
    }
}