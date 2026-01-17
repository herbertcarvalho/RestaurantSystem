using Microsoft.Extensions.DependencyInjection;

namespace Domain.Events;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}

public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent
{
    Task HandleAsync(TEvent domainEvent, CancellationToken ct = default);
}

public class DomainEventPublisher(IServiceProvider serviceProvider)
{

    public async Task PublishAsync<TEvent>(TEvent domainEvent, CancellationToken ct = default)
        where TEvent : IDomainEvent
    {
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(typeof(TEvent));
        var handlers = serviceProvider.GetServices(handlerType);

        foreach (var handler in handlers)
        {
            var method = handlerType.GetMethod("HandleAsync");
            var task = (Task)method.Invoke(handler, new object[] { domainEvent, ct })!;
            await task;
        }
    }
}