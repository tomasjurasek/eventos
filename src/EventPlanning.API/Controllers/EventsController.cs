using EventPlanning.Application.Commands;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace EventPlanning.API.Controllers
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
           
            return result switch
            {
                { IsSuccess: true } => Ok(result.Value.Id),
                _ => BadRequest(result.Errors)
            };
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelEvent(CancelEventCommand cancelEventCommand)
        {
            var result = await _mediator.InvokeAsync<Result<CommandResult>>(cancelEventCommand);

            return result switch
            {
                { IsSuccess: true } => Ok(result.Value.Id),
                _ => BadRequest(result.Errors)
            };
        }
    }
}