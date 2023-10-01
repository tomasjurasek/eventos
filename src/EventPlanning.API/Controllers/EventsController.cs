using AutoMapper;
using EventPlanning.API.Requests.CreateEvent;
using EventPlanning.Application.Commands.CreateEvent;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanning.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EventsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventRequest createEventRequest)
        {
            // TODO Result
            await _mediator.Send(_mapper.Map<CreateEventCommand>(createEventRequest));
            return Ok();
        }
    }
}