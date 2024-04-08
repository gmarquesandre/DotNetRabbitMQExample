using EasyNetQ;
using RabbitMQExample.Core;
using RabbitMQExample.Core.MessageBus;
using RabbitMQExample.Core.RequestResponse;

namespace RabbitMQExample.SecondApi
{
    public class MessageIntegrationHandler : BackgroundService
    {
        private IMessageBus _bus;

        public MessageIntegrationHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
     
           _bus.RespondAsync<MessageIntegrationEvent, ResponseMessage>(
                 async request => 
                new ResponseMessage(await BuscarMensagem(request))

        
                );
           return Task.CompletedTask; 
        }

        private async Task<string> BuscarMensagem(MessageIntegrationEvent requestMessage)
        {

            await Task.Delay(1000);
            return $"{requestMessage.Message} -> Mensagem Respondida {DateTime.Now}";
        }
    }
}
