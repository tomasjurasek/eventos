using FluentResults;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Writer.Contracts.Commands;

namespace Writer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IRequestClient<CreateEvent> _createEventClient;
        private readonly IRequestClient<CancelEvent> _cancelEventClient;

        public EventsController(ILogger<EventsController> logger,
            IRequestClient<CreateEvent> createEventClient,
            IRequestClient<CancelEvent> cancelEventClient)
        {
            _logger = logger;
            _createEventClient = createEventClient;
            _cancelEventClient = cancelEventClient;
        }

        [HttpPost("create-event")]
        public async Task<ActionResult> CreateEvent(CreateEvent createEventCommand)
        {
            var result = await _createEventClient.GetResponse<Result>(createEventCommand);
            if (result.Message.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(result.Message.Errors);
        }

        [HttpPost("cancel-event")]
        public async Task<ActionResult> CreateEvent(CancelEvent cancelEventCommand)
        {
            var result = await _cancelEventClient.GetResponse<Result>(cancelEventCommand);
            if (result.Message.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(result.Message.Errors);
        }
    }
}