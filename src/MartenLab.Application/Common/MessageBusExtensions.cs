using Wolverine;

namespace MartenLab.Application.Common;

public static class MessageBusExtensions
{
    public static Task<TResponse> CommandAsync<TResponse>(this IMessageBus bus, ICommand<TResponse> command)
        => bus.InvokeAsync<TResponse>(command);

    public static Task<TResponse> QueryAsync<TResponse>(this IMessageBus bus, IQuery<TResponse> query)
        => bus.InvokeAsync<TResponse>(query);
}