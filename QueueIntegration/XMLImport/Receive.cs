using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace XMLImport
{
    public class Receive
    {
        private DbInsert _dbInsert = new DbInsert();
        public bool ReceiveMessages()
        {
            try
            {
                var factory = new ConnectionFactory();

                var connection = factory.CreateConnection();

                var channel = connection.CreateModel();
                    
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

                    XDocument xDoc = XDocument.Parse(message);



                    xDoc.Save("temp.xml");
                    XDocument document = XDocument.Load("temp.xml");
                    _dbInsert.DbInserts("temp.xml");
                };

                channel.BasicConsume(queue: "xmlFile",
                    autoAck: true,
                    consumer: consumer);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}