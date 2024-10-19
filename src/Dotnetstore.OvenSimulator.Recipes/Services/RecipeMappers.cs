using Dotnetstore.OvenSimulator.Contracts.Entities;
using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;

namespace Dotnetstore.OvenSimulator.Recipes.Services;

internal static class RecipeMappers
{
    internal static RecipeResponse ToRecipeResponse(this Recipe recipe)
    {
        return new RecipeResponse(
            recipe.Id.Value,
            recipe.Name,
            recipe.HeatCapacity,
            recipe.HeatLossCoefficient,
            recipe.HeaterPowerPercentage,
            recipe.TargetTemperature);
    }   
}