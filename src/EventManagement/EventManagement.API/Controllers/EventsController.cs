using EventManagement.Application.Commands;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using FluentResults.Extensions.AspNetCore;
using Wolverine;

namespace EventManagement.API.Controllers
{
    // TODO API
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IMessageBus _mediator;

        public EventsController(IMessageBus mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateEvent(CreateEventCommand createEventCommand)
        {
            var result = await _mediator.InvokeAsync<Result<CommandResult>>(createEventCommand);
            return result!.ToActionResult();
        }
    }
}