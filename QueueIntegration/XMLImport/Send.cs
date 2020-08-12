using System.IO;
using RabbitMQ.Client;
using System.Text;

namespace XMLImport
{
    public class Send
    {
        private string messages = "";
        public void SendMessage(string fileName)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "xmlFile",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);


                var message = File.ReadAllText(fileName);
                var body = Encoding.UTF8.GetBytes(message);

                messages += message;

                channel.BasicPublish(exchange: "",
                                     routingKey: "xmlFile",
                                     basicProperties: null,
                                     body: body);
            }
        }

        public string Message(string fileName)
        {
            SendMessage(fileName);
            return messages;
        }
    }
}
