using Infrastructure.Services.ReservationServ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
namespace Infrastructure.RabbitMQ.Consumers;

public class RabbitMqConsumer : BackgroundService
{
    private readonly ConnectionFactory _factory;
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly IServiceScopeFactory _scopeFactory;

    public RabbitMqConsumer(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        _factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "booking",
            Password = "booking123"
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _connection = await _factory.CreateConnectionAsync(stoppingToken);
        _channel = await _connection.CreateChannelAsync();

        await _channel.QueueDeclareAsync(
            queue: "no-show",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: stoppingToken);

        // Important: process only one message at a time (avoid overload)
        await _channel.BasicQosAsync(0, 1, false, stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (sender, args) =>
        {
            try
            {
                var body = args.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Received: {json}");

                // Example: deserialize
                var message = JsonSerializer.Deserialize<ICollection<int>>(json);

                // TODO: process message here
                await ProcessAsync(message!);

                // Ack only after success
                await _channel.BasicAckAsync(args.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                // Don't lose the message
                await _channel.BasicNackAsync(args.DeliveryTag, false, requeue: true);
            }
        };

        await _channel.BasicConsumeAsync(
            queue: "no-show",
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);
    }

    private async Task ProcessAsync(ICollection<int> message)
    {
        using var scope = _scopeFactory.CreateScope();
        var reservationService = scope.ServiceProvider
            .GetRequiredService<IReservationService>();

        await reservationService.ProcessNoShowsAsync(message);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_channel is not null)
            await _channel.CloseAsync();

        if (_connection is not null)
            await _connection.CloseAsync();

        await base.StopAsync(cancellationToken);
    }
}
