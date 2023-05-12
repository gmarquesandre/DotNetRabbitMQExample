namespace RabbitMQExample.Core
{
    public class ResponseMessage : Message
    {
        public ResponseMessage(string responseText)
        {
            ResponseText = responseText;
        }

        public string ResponseText { get; private set; }
    }
    
    public abstract class Message
    {
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }
    }
   
}