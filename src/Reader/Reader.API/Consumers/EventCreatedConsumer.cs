using Domain.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Reader.API.DTO;
using System.Text.Json;

namespace Reader.API.Consumers
{
    public class EventCreatedConsumer : IConsumer<EventCreated>
    {
        private readonly IDistributedCache _store;

        public EventCreatedConsumer(IDistributedCache store)
        {
            _store = store;
        }
        public async Task Consume(ConsumeContext<EventCreated> context)
        {
            var @event = new EventDTO
            {
                Name = context.Message.Name,
                IsCanceled = false,
                StartedAt = context.Message.StartedAt,
                FinishedAt = context.Message.FinishedAt,
            };

            await _store.SetStringAsync(context.Message.AggregateId.ToString(), JsonSerializer.Serialize(@event));
        }
    }
}
