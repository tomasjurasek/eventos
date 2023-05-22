using Microsoft.AspNetCore.Mvc;
using ProcessManager.Clients;

namespace ProcessManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class WebhooksController : ControllerBase
    {
        private readonly ILogger<WebhooksController> _logger;
        private readonly IWriterClient _writerClient;

        public WebhooksController(ILogger<WebhooksController> logger, IWriterClient writerClient)
        {
            _logger = logger;
            _writerClient = writerClient;
        }

        [HttpPost("payment-failed")]
        public async Task<ActionResult> Failed([FromBody]PaymentFailed paymentFailed)
        {
            await _writerClient.Cancel(paymentFailed.Id, paymentFailed.Reason);

            return Ok();
        }
    }
}