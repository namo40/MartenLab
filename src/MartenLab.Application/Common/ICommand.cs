using Marten;

namespace MartenLab.Application.Common;

public interface ICommand<out TResponse>;

public interface ICommandHandler<in TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    Task<TResponse> Handle(TCommand command, IDocumentSession session, CancellationToken cancellationToken);
}