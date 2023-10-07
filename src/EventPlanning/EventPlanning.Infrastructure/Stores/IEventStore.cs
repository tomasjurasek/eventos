using EventPlanning.Domain.Common;
using EventPlanning.Infrastructure.Options;
using EventStore.Client;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace EventPlanning.Infrastructure.Stores
{
    internal class EventStore : IEventStore
    {
        private EventStoreClient _store;

        public EventStore(IOptions<EventStoreOptions> eventStoreOptions)
        {
            _store = new EventStoreClient(EventStoreClientSettings.Create(eventStoreOptions.Value.ConnectionString));
        }

        public async Task<ICollection<IDomainEvent>> ReadAsync(Guid streamId)
        {
            var events = await _store
            .ReadStreamAsync(
                Direction.Forwards,
                streamId.ToString(),
                StreamPosition.Start)
            .ToListAsync();

            var parsedEvents = events
                .Select(s => JsonSerializer.Deserialize<IDomainEvent>(Encoding.UTF8.GetString(s.Event.Data.ToArray()))) // TODO Deserialize
                .ToList();

            return parsedEvents;
        }

        public async Task StoreAsync(Guid streamId, int streamLastVersion, ICollection<IDomainEvent> events)
        {
            var streamResult = _store.ReadStreamAsync(Direction.Forwards, streamId.ToString(), StreamPosition.End, 1);
            var lastEvent = await streamResult.LastAsync();

            if (lastEvent.Event.EventNumber.ToInt64() > streamLastVersion)
            {
                throw new InvalidOperationException("TODO"); // TODO Result or Exception ??
            }
            else
            {
                var eventData = events
                 .Select(s =>
                  new EventData(Uuid.FromGuid(streamId), s.GetType().Name,
                  new ReadOnlyMemory<byte>(JsonSerializer.SerializeToUtf8Bytes(s, s.GetType()))));


                await _store.AppendToStreamAsync(streamId.ToString(), StreamState.Any, eventData);
            }
        }
    }

    public interface IEventStore
    {
        Task StoreAsync(Guid streamId, int streamLastVersion, ICollection<IDomainEvent> events);
        Task<ICollection<IDomainEvent>> ReadAsync(Guid streamId);
    }
}
