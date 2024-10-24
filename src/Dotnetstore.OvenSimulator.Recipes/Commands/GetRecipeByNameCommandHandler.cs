using Ardalis.Result;
using Dotnetstore.OvenSimulator.Contracts.Commands;
using Dotnetstore.OvenSimulator.Recipes.Services;
using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;
using MediatR;

namespace Dotnetstore.OvenSimulator.Recipes.Commands;

internal sealed class GetRecipeByNameCommandHandler(IRecipeService recipeService) : IRequestHandler<GetRecipeByNameCommand, Result<RecipeResponse?>>
{
    async Task<Result<RecipeResponse?>> IRequestHandler<GetRecipeByNameCommand, Result<RecipeResponse?>>
        .Handle(GetRecipeByNameCommand request, CancellationToken cancellationToken)
    {
        var result = await recipeService.GetByNameAsync(request.Name, cancellationToken);
        
        return result.IsSuccess
            ? Result<RecipeResponse?>.Success(result.Value)
            : Result<RecipeResponse?>.NotFound(string.Join(", ", result.Errors.Select(x => x)));
    }
}