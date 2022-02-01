using System.Text;
using RabbitMQ.Client;

namespace invoice.app.Services;

public class QueueClientService
{
    private readonly ILogger<QueueClientService> _logger;
    private readonly ConnectionFactory _mqFactory;

    public QueueClientService(
        ILogger<QueueClientService> logger)
    {
        _logger = logger;
        _mqFactory = new ConnectionFactory() { HostName = "localhost" };
        
    }

    public void SendMessage(string message)
    {
        using var connection = _mqFactory.CreateConnection();
        if(connection.IsOpen)
        {
            _logger.LogInformation("Connection is open to RabbitMQ");
        }

        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "hello",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);
                                
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
    }
}