using PurchaseServiceDomain.SharedKernel;

namespace PurchaseServiceApplication.MessageBus.Interfaces
{
    public interface IMessageProducer
    {
        public void SendingMessage<T>(Event<T> @event, string queueName);
    }
}
