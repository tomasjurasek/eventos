using FluentResults;
using Simplife.EventSourcing.Aggregates;
using EventManagement.Domain.Common;
using EventManagement.Domain.Event.Events;
using Marten;
using System.Text.Json;
using Marten.Events;
using Newtonsoft.Json;

namespace EventManagement.Infrastructure.Repositories
{
    internal class AggregateRootRepository<TAggregate> : IAggregateRootRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        private readonly IDocumentStore _store;

        public AggregateRootRepository(IDocumentStore documentStore)
        {
            _store = documentStore;
            // TODO
           // _store = DocumentStore.For(_ =>
           //{
           //    _.DatabaseSchemaName = "events";
           //    _.Connection(eventStoreOptions.Value.ConnectionString);
           //    _.AutoCreateSchemaObjects = AutoCreate.None;

           //});
        }

        public async Task<Result<TAggregate>> FindAsync(Guid Id)
        {
            var aggregate = default(IAggregateRoot);
            await using var session = _store.LightweightSession();
            var events = await session.Events.FetchStreamAsync(Id);
            // TODO
            //events.Select(s => aggregate.Rehydrate(JsonConvert.DeserializeObject(s.Data, s.EventType));
            return Result.Ok();
        }

        public async Task<Result> StoreAsync(TAggregate aggregate)
        {
            await using var session = _store.LightweightSession();

            // TODO
            session.Events.Append(aggregate.Id, aggregate.GetUncommittedEvents());
            await session.SaveChangesAsync();
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
