using Microsoft.Extensions.DependencyInjection;

namespace Application.Interfaces;

public interface ICommand<TResult> { }

public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
    Task<TResult> HandleAsync(TCommand command, CancellationToken ct = default);
}

public class CommandDispatcher(IServiceProvider serviceProvider)
{
    public async Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken ct = default)
        where TCommand : ICommand<TResult>
    {
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(typeof(TCommand), typeof(TResult));
        var handler = serviceProvider.GetServices(handlerType);

        var method = handlerType.GetMethod("HandleAsync");
        var task = (Task<TResult>)method!.Invoke(handler, [command, ct])!;

        return await task;
    }
}