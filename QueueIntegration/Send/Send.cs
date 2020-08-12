using System;
using System.IO;
using RabbitMQ.Client;
using System.Text;

namespace Send
{
    public class Send
    {
        private readonly GetFileList _fileList = new GetFileList();
        private void SendMessage()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using var connection = factory.CreateConnection();
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "xmlFile",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                foreach (var item in _fileList.GetAllFilesToList())
                {
                    var file = File.ReadAllText(item);

                    var body = Encoding.UTF8.GetBytes(file);

                    channel.BasicPublish(exchange: "",
                        routingKey: "xmlFile",
                        basicProperties: null,
                        body: body);
                }
            }

            Console.WriteLine(" Press [enter] to exit");
            Console.ReadKey();
        }

        public static void Main()
        {
            var send = new Send();
            send.SendMessage();
        }
    }
}