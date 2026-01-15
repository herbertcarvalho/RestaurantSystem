namespace Application.Interfaces;

// Interface base para Queries
public interface IQuery<TResult> { }
// Interface base para Query Handlers
public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken ct = default);
}
