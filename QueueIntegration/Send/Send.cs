using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RabbitMQ.Client;
using System.Text;

namespace Send
{
    class Send
    {
        private string filePath = @"C:/Users/Konrad/Desktop/euvic/";
            
        private List<string> _filePathList = new List<string>();


        private List<string> GetAllFiles(List<string> list)
        {
            var localFiles = Directory.GetFiles(filePath,
                "*.xml",
                SearchOption.TopDirectoryOnly).ToList();

            foreach (var link in localFiles)
            {
                list.Add(link);
            }

            return list;
        }
        
        
        
        private void SendMessage()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "xmlFile",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var file = File.ReadAllText(filePath);

                    var body = Encoding.UTF8.GetBytes(file);

                    channel.BasicPublish(exchange: "",
                        routingKey: "xmlFile",
                        basicProperties: null,
                        body: body);
                }

                Console.WriteLine(" Press [enter] to exit");
                Console.ReadKey();
            }
        }

        public static void Main()
        {
            var send = new Send();
            send.SendMessage();
        }
    }
}