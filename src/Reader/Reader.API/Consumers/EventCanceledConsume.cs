using Domain.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Reader.API.DTO;
using System.Text.Json;

namespace Reader.API.Consumers
{
    public class EventCanceledConsumer : IConsumer<EventCanceled>
    {
        private readonly IDistributedCache _store;

        public EventCanceledConsumer(IDistributedCache store)
        {
            _store = store;
        }
        public async Task Consume(ConsumeContext<EventCanceled> context)
        {
            var @event = JsonSerializer.Deserialize<EventDTO>(await _store.GetStringAsync(context.Message.AggregateId.ToString()));

            @event = @event with { IsCanceled = true };

            await _store.SetStringAsync(context.Message.AggregateId.ToString(), JsonSerializer.Serialize(@event));
        }
    }
}
