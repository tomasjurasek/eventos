using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Reader.API.DTO;
using System.Text.Json;
using Writer.Contracts.Events;

namespace Reader.API.EventHandlers
{
    public class EventHandler :
         IConsumer<EventCreated>,
         IConsumer<EventCanceled>

    {
        private readonly IDistributedCache _cache;

        public EventHandler(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task Consume(ConsumeContext<EventCanceled> context)
        {
            var @event = JsonSerializer.Deserialize<EventDTO>(await _cache.GetStringAsync(context.Message.Id.ToString()));

            @event.CanceledAt = context.Message.CanceledAt;
            @event.UpdatedAt = DateTimeOffset.UtcNow;

            await _cache.SetStringAsync(context.Message.Id.ToString(), JsonSerializer.Serialize(@event));
        }

        public async Task Consume(ConsumeContext<EventCreated> context)
        {
            var @event = new EventDTO
            {
                Id = context.Message.Id,
                CreatedAt = context.Message.CreatedAt,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            await _cache.SetStringAsync(context.Message.Id.ToString(), JsonSerializer.Serialize(@event));

        }
    }
}
