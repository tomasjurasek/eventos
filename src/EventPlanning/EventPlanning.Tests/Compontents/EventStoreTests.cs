using Testcontainers.EventStoreDb;

namespace EventPlanning.Tests.Compontents
{
    public class EventStoreTests
    {
        private readonly EventStoreDbContainer _eventStoreDbContainer;
        private readonly Infrastructure.Stores.EventStore _eventStore;
        public EventStoreTests()
        {


            //_eventStore = new Infrastructure.Stores.EventStore(Options.Create(new Infrastructure.Options.EventStoreOptions
            //{
            //    ConnectionString = ""
            //}));
        }

        [Fact]
        public async Task Test()
        {
            var eventStoreDbContainer = new EventStoreDbBuilder()
                .WithImage("eventstore/eventstore:21.2.0-buster-slim")
                .WithName("aaa").Build();


            await eventStoreDbContainer.StartAsync();
        }
    }
}
