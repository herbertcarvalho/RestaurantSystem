using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Infrastructure.RabbitMQ.Publishers;

public class RabbitMqPublisher : IRabbitMqPublisher
{
    private readonly ConnectionFactory _factory;

    public RabbitMqPublisher()
    {
        _factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "booking",
            Password = "booking123"
        };
    }

    public async Task Publish<T>(string queue, T message)
    {
        using var connection = await _factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        // Ensure queue exists
        await channel.QueueDeclareAsync(
            queue: queue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        var props = new BasicProperties
        {
            Persistent = true
        };

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: queue,
            mandatory: false,
            basicProperties: props,
            body: body
        );
    }
}
