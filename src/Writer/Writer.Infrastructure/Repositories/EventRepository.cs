using Domain.Events;
using EventStore.Client;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using Writer.Domain.Repositories;
using Writer.Infrastructure.Settings;

namespace Writer.Infrastructure.Repositories
{
    internal class EventRepository : IEventRepository
    {
        private readonly EventStoreClient _store;

        public EventRepository(IOptions<EventStoreSettings> settings)
        {
            _store = new EventStoreClient(EventStoreClientSettings.Create(settings.Value.ConnectionString));
        }

        public async Task<Domain.Aggregates.Event> FindAsync(Guid aggregateId)
        {
            var events = await _store
              .ReadStreamAsync(
                  Direction.Forwards,
                  aggregateId.ToString(),
                  StreamPosition.Start)
              .ToListAsync();

            var parsedEvents = events
                .Select(s => (IEvent)JsonSerializer.Deserialize(Encoding.UTF8.GetString(s.Event.Data.ToArray()), GetType(s.Event.EventType)))
                .ToList();

            return new Domain.Aggregates.Event(parsedEvents!);
        }

        public async Task StoreAsync(Domain.Aggregates.Event aggregate)
        {
            var eventData = aggregate.UncommitedEvents
              .Select(s =>
               new EventData(Uuid.FromGuid(aggregate.Id), s.GetType().Name,
               new ReadOnlyMemory<byte>(JsonSerializer.SerializeToUtf8Bytes(s, s.GetType()))));


            await _store.AppendToStreamAsync(aggregate.Id.ToString(), StreamState.Any, eventData);
        }

        public static Type GetType(string type) => type switch
        {
            nameof(EventCreated) => typeof(EventCreated),
            nameof(EventCanceled) => typeof(EventCanceled),
        };
    }
}
