using FluentResults;
using Microsoft.Extensions.Options;
using Simplife.EventSourcing.Aggregates;
using WriteModel.Domain.Common;
using WriteModel.Domain.Event.Events;
using WriteModel.Infrastructure.Options;
using Marten;
using Weasel.Core;

namespace WriteModel.Infrastructure.Repositories
{
    internal class AggregateRootRepository<TAggregate> : IAggregateRootRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        private readonly DocumentStore _store;

        public AggregateRootRepository(IOptions<EventStoreOptions> eventStoreOptions)
        {
            // TODO
            _store = DocumentStore.For(_ =>
           {
               _.DatabaseSchemaName = "events";
               _.Connection(eventStoreOptions.Value.ConnectionString);
               _.AutoCreateSchemaObjects = AutoCreate.None;

           });
        }

        public async Task<Result<TAggregate>> FindAsync(Guid Id)
        {
            var aggregate = default(IAggregateRoot);
            await using var session = _store.LightweightSession();
            var events = await session.Events.FetchStreamAsync(Id);

            // TODO
            //events.Select(s => aggregate.Rehydrate(s.Da))
            return Result.Ok();
        }

        public async Task<Result> StoreAsync(TAggregate aggregate)
        {
            await using var session = _store.LightweightSession();

            // TODO
            //session.Events.AppendExclusive();
            return Result.Ok();

        }

        //TODO Dynamic??
        private Type GetTypeFromEvent(string eventType) => eventType switch
        {
            nameof(EventCreated) => typeof(EventCreated),
            nameof(EventCanceled) => typeof(EventCanceled),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }
}
