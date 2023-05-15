using EasyNetQ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQExample.Core;
using RabbitMQExample.Core.MessageBus;
using RabbitMQExample.Core.RequestResponse;

namespace RabbitMQExample.FirstApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        private IMessageBus _bus;

        public TesteController(IMessageBus bus)
        {
            _bus = bus;
        }

        [HttpGet("TesteRabbit")]
        public async Task<IActionResult> Teste()
        {
            MessageIntegrationEvent message = new(Guid.NewGuid(), $"Mensagem enviada {DateTime.Now}");
            
            var sucesso = await _bus.RequestAsync<MessageIntegrationEvent, ResponseMessage>(message);

            return Ok(sucesso);
        }
    }
}
