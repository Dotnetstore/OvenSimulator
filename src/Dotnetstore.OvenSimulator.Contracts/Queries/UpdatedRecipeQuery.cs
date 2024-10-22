using Dotnetstore.OvenSimulator.Contracts.Entities;
using Dotnetstore.OvenSimulator.SDK.Recipes.Requests;
using MediatR;

namespace Dotnetstore.OvenSimulator.Contracts.Queries;

public record struct UpdatedRecipeQuery(
    UpdateRecipeRequest UpdateRecipeRequest,
    Recipe OldRecipe) : IRequest;