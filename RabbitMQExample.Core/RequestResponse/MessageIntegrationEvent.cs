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
        protected MessageIntegrationEvent()
        {
            
        }
        public MessageIntegrationEvent(Guid id, string mensagem)
        {
            Id = id;
            Message = mensagem;
        }

        public Guid Id { get; set; }
        public string Message { get; set; }

    }
}