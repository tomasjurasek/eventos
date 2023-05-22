using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Reader.API.DTO;
using System.Text.Json;

namespace Reader.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IDistributedCache _store;

        public EventsController(ILogger<EventsController> logger,
            IDistributedCache store)
        {
            _logger = logger;
            _store = store;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _store.GetStringAsync(id.ToString()));
        }
    }
}