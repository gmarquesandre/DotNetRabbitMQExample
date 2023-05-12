namespace RabbitMQExample.Core.RequestResponse
{
    public abstract class IntegrationEvent : Event
    {

    }

    public class Event
    {

    }


    public class MessageIntegrationEvent : IntegrationEvent
    {
        public MessageIntegrationEvent(Guid id, string mensagem)
        {
            Id = id;
            Message = mensagem;
        }

        public Guid Id { get; private set; }
        public string Message { get; private set; }

    }
}