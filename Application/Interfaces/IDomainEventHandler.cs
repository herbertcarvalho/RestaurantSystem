using Microsoft.Extensions.DependencyInjection;

namespace Application.Interfaces;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent
{
    Task HandleAsync(TEvent domainEvent, CancellationToken ct = default);
}
public class DomainEventPublisher
{
    private readonly IServiceProvider _serviceProvider;
    public async Task PublishAsync<TEvent>(TEvent domainEvent, CancellationToken ct = default)
        where TEvent : IDomainEvent
    {
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(typeof(TEvent));
        var handlers = _serviceProvider.GetServices(handlerType);
        foreach (var handler in handlers)
        {
            var method = handlerType.GetMethod("HandleAsync");
            var task = (Task)method!.Invoke(handler, new object[] { domainEvent, ct })!;
            await task;
        }
    }
}
