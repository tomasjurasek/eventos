using EventStore.Client;
using System.Text.Json;
using System.Text;
using Writer.Contracts.Events;
using Writer.Domain.Repositories;
using Microsoft.Extensions.Options;
using Writer.API;

namespace Writer.Application.Infrastructure
{
    internal class EventRepository : IEventRepository
    {
        private readonly EventStoreClient _store;

        public EventRepository(IOptions<EventStoreSettings> settings)
        {
            _store = new EventStoreClient(EventStoreClientSettings.Create(settings.Value.ConnectionString));
        }

        public async Task<Domain.Aggregates.Event> GetAsync(Guid id)
        {
            // PER 300 eventu
            // Snapshot STORE - REDIS
            // GET snapshot by ID
            // Aggregate s version 300


            var events = await _store
                .ReadStreamAsync(
                    Direction.Forwards,
                    id.ToString(),
                    StreamPosition.Start)
                .ToListAsync();

            var parsedEvents = events
                .Select(s => (IEvent)JsonSerializer.Deserialize(Encoding.UTF8.GetString(s.Event.Data.ToArray()), EventTypeHelper.GetType(s.Event.EventType)))
                .ToList();

            return new Domain.Aggregates.Event(parsedEvents!);
        }

        public async Task StoreAsync(Domain.Aggregates.Event aggregate)
        {
            var eventData = aggregate.UncommitedEvents
               .Select(s =>
                new EventData(Uuid.FromGuid(s.Id), s.GetType().Name,
                new ReadOnlyMemory<byte>(JsonSerializer.SerializeToUtf8Bytes(s))));


            await _store.AppendToStreamAsync(aggregate.Id.ToString(), StreamState.Any, eventData);
        }
    }
}
