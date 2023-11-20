using EventPlanning.Domain.Common;
using EventStore.Client;
using FluentResults;
using System.Text.Json;
using System.Text;
using EventPlanning.Infrastructure.Options;
using Microsoft.Extensions.Options;
using EventPlanning.Domain.Event.Events;
using System.Reflection;
using Simplife.EventSourcing.Aggregates;
using Simplife.Core.Events;

namespace EventPlanning.Infrastructure.Repositories
{
    internal class AggregateRootRepository<TAggregate> : IAggregateRootRepository<TAggregate> where TAggregate : IAggregateRoot, new ()
    {
        private EventStoreClient _store;

        public AggregateRootRepository(IOptions<EventStoreOptions> eventStoreOptions)
        {
            _store = new EventStoreClient(EventStoreClientSettings.Create(eventStoreOptions.Value.ConnectionString));
        }

        public async Task<Result<TAggregate>> FindAsync(Guid Id)
        {
            var aggregate = CreateEmptyAggregate();
            try
            {
                var events = await _store
                    .ReadStreamAsync(Direction.Forwards, Id.ToString(), StreamPosition.Start)
                    .ToListAsync();

                var parsedEvents = events
                    .Select(s => (IEvent)JsonSerializer.Deserialize(Encoding.UTF8.GetString(s.Event.Data.ToArray()), GetTypeFromEvent(s.Event.EventType)))
                    .ToList();

                if (parsedEvents is null)
                {
                    return Result.Fail("TODO");
                }

                aggregate.Rehydrate(parsedEvents);
                return aggregate;
            }

            catch (StreamNotFoundException)
            {
                return Result.Fail("TODO");
            }
        }

        public async Task<Result> StoreAsync(TAggregate aggregate)
        {
            try
            {
                var streamResult = _store.ReadStreamAsync(Direction.Forwards, aggregate.Id.ToString(), StreamPosition.End, 1);
                var lastEvent = await streamResult.LastAsync();

                if (lastEvent.Event.EventNumber.ToInt64() > aggregate.Version)
                {
                    return Result.Fail("TODO ERROR");
                }

                var eventData = aggregate.GetUncommittedEvents()
                 .Select(s =>
                  new EventData(Uuid.FromGuid(aggregate.Id), s.GetType().Name, JsonSerializer.SerializeToUtf8Bytes(s, s.GetType()).AsMemory()));


                await _store.AppendToStreamAsync(aggregate.Id.ToString(), StreamState.Any, eventData);


                return Result.Ok();
            }
            catch (Exception)
            {
                // LOG
                return Result.Fail("TODO ERROR");
            }
        }


        private TAggregate CreateEmptyAggregate() => (TAggregate)typeof(TAggregate)
              .GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                null, [], Array.Empty<ParameterModifier>())
              .Invoke([]);

        //TODO Dynamic??
        private Type GetTypeFromEvent(string eventType) => eventType switch
        {
            nameof(EventCreated) => typeof(EventCreated),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }
}
