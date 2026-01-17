namespace Infrastructure.RabbitMQ.Publishers;

public interface IRabbitMqPublisher
{
    Task Publish<T>(string queue, T message);
}
