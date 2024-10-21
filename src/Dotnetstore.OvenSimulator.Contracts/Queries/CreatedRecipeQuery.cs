using Dotnetstore.OvenSimulator.SDK.Recipes.Requests;
using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;
using MediatR;

namespace Dotnetstore.OvenSimulator.Contracts.Queries;

public record struct CreatedRecipeQuery(
    CreateRecipeRequest CreateRecipeRequest,
    RecipeResponse RecipeResponse) : IRequest;