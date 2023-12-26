using EventPlanning.Writer.Application.Commands;
using FluentResults;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanning.Writer.API.Controllers
{
    // TODO API
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventCommand createEventCommand)
        {
            var client = _mediator.CreateRequestClient<CreateEventCommand>();
            var result = await client.GetResponse<Result<CommandResult>>(createEventCommand);

            return result.Message switch
            {
                { IsSuccess: true } => Ok(result.Message.Value.Id),
                _ => BadRequest(result.Message.Errors)
            };
        }
    }
}