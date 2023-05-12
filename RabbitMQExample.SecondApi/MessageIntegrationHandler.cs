using EasyNetQ;
using RabbitMQExample.Core;
using RabbitMQExample.Core.RequestResponse;

namespace RabbitMQExample.SecondApi
{
    public class MessageIntegrationHandler : BackgroundService
    {
        private IBus _bus;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus = RabbitHutch.CreateBus("host=localhost:5672");
            _bus.Rpc.RespondAsync<MessageIntegrationEvent, ResponseMessage>(
                async request => 
                new ResponseMessage(await BuscarMensagem())

        
                );
           return Task.CompletedTask; 
        }

        private async Task<string> BuscarMensagem()
        {
            await Task.Delay(1000);
            return $"Mensagem Respondida {DateTime.Now}";
        }
    }
}
