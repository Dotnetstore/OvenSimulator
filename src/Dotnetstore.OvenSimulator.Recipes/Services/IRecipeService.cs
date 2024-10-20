﻿using Ardalis.Result;
using Dotnetstore.OvenSimulator.SDK.Recipes.Requests;
using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;

namespace Dotnetstore.OvenSimulator.Recipes.Services;

internal interface IRecipeService
{
    ValueTask<IEnumerable<RecipeResponse>> GetAllAsync(CancellationToken ct);
    
    ValueTask<Result<RecipeResponse?>> GetByIdAsync(Guid id, CancellationToken ct);
    
    ValueTask<Result<RecipeResponse?>> GetByNameAsync(string name, CancellationToken ct);
    
    ValueTask<Result<RecipeResponse?>> CreateAsync(CreateRecipeRequest request, CancellationToken ct);
}