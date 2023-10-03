using AutoMapper;
using EventPlanning.API.Requests.CreateEvent;
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
        private readonly IMapper _mapper;

        public EventsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventRequest createEventRequest)
        {
            var client = _mediator.CreateRequestClient<CreateEventCommand>();

            var result = await client.GetResponse<Result>(_mapper.Map<CreateEventCommand>(createEventRequest));
            if (result.Message.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message.Errors);
            }
        }
    }
}