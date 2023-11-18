using EventPlanning.Application.Commands.CreateEvent;
using FluentResults;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanning.API.Controllers
{
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

            var result = await client.GetResponse<Result>(createEventCommand);

            return result switch
            {
                { Message.IsSuccess: true } => Ok(),
                _ => BadRequest(result.Message.Errors)
            };
        }
    }
}