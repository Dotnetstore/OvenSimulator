using MediatR;

namespace Dotnetstore.OvenSimulator.Contracts.Queries;

public record struct DeletedRecipeQuery(Guid Id) : IRequest;