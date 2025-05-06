using Marten;

namespace MartenLab.Application.Common;

public interface IQuery<out TResponse>;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<TResponse> Handle(TQuery query, IQuerySession session, CancellationToken cancellationToken);
}