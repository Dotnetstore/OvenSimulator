using Dotnetstore.OvenSimulator.Recipes.Data;
using Dotnetstore.OvenSimulator.SDK.Users.Requests;
using Dotnetstore.OvenSimulator.SharedKernel.Extensions;
using Dotnetstore.OvenSimulator.Users;
using FastEndpoints.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace Dotnetstore.OvenSimulator.WebAPI.Tests.Integration;

[DisableWafCache]
public class DotnetstoreOvenSimulatorBase : AppFixture<Program>
{
    private IServiceCollection _services = null!;
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder().Build();

    public readonly string ApiKey = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyTmFtZSI6InRlc3RAdGVzdC5jb20iLCJVc2VySWQiOiIyNDkyNjE1NS1jMDVlLTQ1NzgtYWZkZi0yYWVkOWQ4ZmMxNDEiLCJyb2xlIjoiT3BlcmF0b3IiLCJleHAiOjIyMDMxNDI2NjIsImlhdCI6MTcyOTg0MzQ2MiwibmJmIjoxNzI5ODQzNDYyfQ.P6uEySWuTWehaxQ6Ov2qWAGoVSFB8Mj0avJl3Jc6vzI";

    protected override async Task PreSetupAsync()
    {
        await Task.CompletedTask;
    }

    protected override Task SetupAsync()
    {
        return Task.CompletedTask;
    }

    protected override void ConfigureApp(IWebHostBuilder a)
    {
        _msSqlContainer.StartAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        
        a.ConfigureTestServices(x =>
        {
            _services = x;
            
            _services.RemoveDbContext<RecipeDataContext>();
            _services.AddDbContext<RecipeDataContext>(_msSqlContainer.GetConnectionString());
            _services.EnsureDbCreated<RecipeDataContext>();
        });
    }

    protected override void ConfigureServices(IServiceCollection s)
    {
    }

    protected override Task TearDownAsync()
    {
        _msSqlContainer.StopAsync();
        return Task.CompletedTask;
    }
}