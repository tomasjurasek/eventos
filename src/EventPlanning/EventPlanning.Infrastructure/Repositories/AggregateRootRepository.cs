using EventPlanning.Domain.Common;
using EventStore.Client;
using FluentResults;
using System.Text.Json;
using System.Text;
using EventPlanning.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Simplife.EventSourcing.Aggregates;
using Simplife.Core.Events;
using EventPlanning.Domain.Event.Events;

namespace EventPlanning.Infrastructure.Repositories
{
    internal class AggregateRootRepository<TAggregate> : IAggregateRootRepository<TAggregate> where TAggregate : IAggregateRoot, new()
    {
        private EventStoreClient _store;

        public AggregateRootRepository(IOptions<EventStoreOptions> eventStoreOptions)
        {
            _store = new EventStoreClient(EventStoreClientSettings.Create(eventStoreOptions.Value.ConnectionString));
        }

        public async Task<Result<TAggregate>> FindAsync(Guid Id)
        {
            var aggregate = default(TAggregate);
            try
            {
                var events = await _store
                    .ReadStreamAsync(Direction.Forwards, Id.ToString(), StreamPosition.Start)
                    .ToListAsync();

                var parsedEvents = events
                    .Select(s => (IEvent)JsonSerializer.Deserialize(s.Event.Data.Span, GetTypeFromEvent(s.Event.EventType)))
                    .ToList();

                if (parsedEvents is null)
                {
                    return Result.Fail("ENTITY_NOT_FOUND");
                }

                aggregate!.Rehydrate(parsedEvents!);
                return aggregate;
            }

            catch (StreamNotFoundException)
            {
                return Result.Fail("ENTITY_NOT_FOUND");
            }
        }

        public async Task<Result> StoreAsync(TAggregate aggregate)
        {
            var streamResult = _store.ReadStreamAsync(Direction.Forwards, aggregate.Id.ToString(), StreamPosition.End, 1);

            long aggregateVersion = streamResult.LastStreamPosition?.ToInt64() ?? 0;

            if (aggregateVersion > aggregate.Version)
            {
                return Result.Fail("NEW_VERSION_EXISTS");
            }

            var eventData = aggregate.GetUncommittedEvents()
             .Select(s =>
                new EventData(
                    Uuid.FromGuid(aggregate.Id),
                    s.GetType().Name,
                    JsonSerializer.SerializeToUtf8Bytes(s, s.GetType()).AsMemory()));


            await _store.AppendToStreamAsync(aggregate.Id.ToString(), StreamState.Any, eventData);

            return Result.Ok();

        }

        //TODO Dynamic??
        private Type GetTypeFromEvent(string eventType) => eventType switch
        {
            nameof(EventCreated) => typeof(EventCreated),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }
}
