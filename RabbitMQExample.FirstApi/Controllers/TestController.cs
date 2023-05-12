using EasyNetQ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQExample.Core;
using RabbitMQExample.Core.RequestResponse;

namespace RabbitMQExample.FirstApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        private IBus _bus;

        [HttpGet("TesteRabbit")]
        public async Task<IActionResult> Teste()
        {
            MessageIntegrationEvent message = new(Guid.NewGuid(), $"Mensagem enviada {DateTime.Now}");

            _bus = RabbitHutch.CreateBus("host=localhost:5672");

            var sucesso = await _bus.Rpc.RequestAsync<MessageIntegrationEvent, ResponseMessage>(message);

            return Ok(sucesso);
        }
    }
}
