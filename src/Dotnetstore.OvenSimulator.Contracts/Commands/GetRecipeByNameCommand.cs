using Ardalis.Result;
using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;
using MediatR;

namespace Dotnetstore.OvenSimulator.Contracts.Commands;

public record struct GetRecipeByNameCommand(string Name) : IRequest<Result<RecipeResponse?>>;