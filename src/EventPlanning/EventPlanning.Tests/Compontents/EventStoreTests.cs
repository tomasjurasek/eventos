using Microsoft.Extensions.Options;
using Testcontainers.EventStoreDb;

namespace EventPlanning.Tests.Compontents
{
    public class EventStoreTests
    {
        private readonly Infrastructure.Stores.EventStore _eventStore;
        public EventStoreTests()
        {
            var container = new EventStoreDbBuilder().Build();
            container.StartAsync().Wait();

            _eventStore = new Infrastructure.Stores.EventStore(Options.Create(new Infrastructure.Options.EventStoreOptions
            {
                ConnectionString = container.GetConnectionString()
            }));
        }

        [Fact]
        public async Task Test()
        {
            var events = await _eventStore.ReadAsync(Guid.NewGuid());
        }
    }
}
