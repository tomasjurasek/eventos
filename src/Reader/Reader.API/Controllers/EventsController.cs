using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Reader.API.DTO;

namespace Reader.API.Controllers
{
    [ApiController]
    [Route("events")]
    public class EventsController : ControllerBase
    {
        private readonly IDistributedCache _store;

        public EventsController(IDistributedCache store)
        {
            _store = store;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<EventDTO>> Get(string id)
        {
            var json = await _store.GetStringAsync(id);

            if (json is null)
            {
                return NoContent();
            }

            return Ok(json);

        }
    }
}