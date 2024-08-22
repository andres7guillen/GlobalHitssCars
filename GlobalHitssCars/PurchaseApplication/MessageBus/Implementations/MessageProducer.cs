using PurchaseServiceApplication.MessageBus.Interfaces;
using PurchaseServiceDomain.SharedKernel;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PurchaseServiceApplication.MessageBus.Implementations
{
    public class MessageProducer : IMessageProducer
    {
        public void SendingMessage<T>(Event<T> @event, string queueName)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "(1234567890)",
                VirtualHost = "/"
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queueName, durable: true, exclusive: true);

            var jsonString = JsonSerializer.Serialize(@event);
            var body = Encoding.UTF8.GetBytes(jsonString);
            channel.BasicPublish("", queueName, body: body);
        }
    }
}
