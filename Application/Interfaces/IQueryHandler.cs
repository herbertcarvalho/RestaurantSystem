namespace Application.Interfaces;

// Interface base para Queries
public interface IQuery<TResult> { }
// Interface base para Query Handlers
public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken ct = default);
}

public class QueryDispatcher(IServiceProvider serviceProvider)
{
    public async Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken ct = default)
        where TCommand : IQuery<TResult>
    {
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(typeof(TCommand), typeof(TResult));
        var handler = serviceProvider.GetService(handlerType);

        var method = handlerType.GetMethod("HandleAsync");
        var task = (Task<TResult>)method!.Invoke(handler, [command, ct])!;

        return await task;
    }
}
