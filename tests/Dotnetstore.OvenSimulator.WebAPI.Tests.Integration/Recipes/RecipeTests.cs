using Dotnetstore.OvenSimulator.Recipes.GetAll;
using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Dotnetstore.OvenSimulator.WebAPI.Tests.Integration.Recipes;

public class RecipeTests(DotnetstoreOvenSimulatorBase simulatorBase) : TestBase<DotnetstoreOvenSimulatorBase>
{
    [Fact]
    public async Task GetAllRecipes_ShouldReturn2Objects()
    {
        //Act
        var (rsp, res) = await simulatorBase.Client.GETAsync<GetAllRecipeEndpoint, IEnumerable<RecipeResponse>>();
        
        //Assert
        using (new AssertionScope())
        {
            rsp.Should().BeSuccessful();
            res.Should().HaveCount(1);
        }
    }
}