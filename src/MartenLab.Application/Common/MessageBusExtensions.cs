using Wolverine;

namespace MartenLab.Application.Common;

public static class MessageBusExtensions
{
    public static Task<TResponse> CommandAsync<TResponse>(this IMessageBus bus, ICommand<TResponse> command)
        => bus.InvokeAsync<TResponse>(command);
}