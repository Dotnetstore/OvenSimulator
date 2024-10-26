using MediatR;

namespace Dotnetstore.OvenSimulator.Contracts.Queries;

public record struct SendOvenErrorEmailQuery(string ErrorMessage) : IRequest;