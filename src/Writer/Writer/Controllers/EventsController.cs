using FluentResults;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Writer.Application.Handlers.CreateEvent;

namespace Writer.Controllers
{
    [ApiController]
    [Route("events")]
    public class EventsController : ControllerBase
    {
        private readonly IRequestClient<CreateEventCommand> _createEventCommandHandlerClient;

        public EventsController(IRequestClient<CreateEventCommand> createEventCommandHandlerClient)
        {
            _createEventCommandHandlerClient = createEventCommandHandlerClient;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(Guid), 200)]
        [ProducesResponseType(typeof(IList<IError>), 400)]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand command)
        {
            var result = await _createEventCommandHandlerClient.GetResponse<Result>(command);

            if (result.Message.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(result.Message.Errors);
        }

    }
}