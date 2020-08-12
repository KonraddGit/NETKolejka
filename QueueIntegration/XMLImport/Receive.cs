using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace XMLImport
{
    public class Receive
    {
        public bool ReceiveMessage()
        {
            try
            {
                var factory = new ConnectionFactory();

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: "xmlFile",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                        var consumer = new EventingBasicConsumer(channel);

                        consumer.Received += (model, args) =>
                        {
                            var body = args.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);
                        };

                        channel.BasicConsume(queue: "xmlFile",
                            autoAck: true,
                            consumer: consumer);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SerializeXml()
        {

        }
    }
}