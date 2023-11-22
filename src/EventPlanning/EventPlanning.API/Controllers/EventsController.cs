using EventPlanning.Application.Commands;
using EventPlanning.Application.Commands.CreateEvent;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace EventPlanning.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IMessageBus _messageBus;

        public EventsController(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventCommand createEventCommand)
        {
            var result = await _messageBus.InvokeAsync<Result<CommandResult>>(createEventCommand);

            return result switch
            {
                { IsSuccess: true } => Ok(),
                _ => BadRequest(result.Errors)
            };
        }
    }
}