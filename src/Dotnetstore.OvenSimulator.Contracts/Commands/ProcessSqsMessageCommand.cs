using MediatR;

namespace Dotnetstore.OvenSimulator.Contracts.Commands;

public record struct ProcessSqsMessageCommand : IRequest;