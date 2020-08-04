using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

class Receive
{
    public static void Main()
    {
        var factory = new ConnectionFactory();

        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
            }
        }
    }
}