using FluentAssertions;
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
            container.StartAsync().Wait(); // IAsyncLifetime or some Init

            _eventStore = new Infrastructure.Stores.EventStore(Options.Create(new Infrastructure.Options.EventStoreOptions
            {
                ConnectionString = container.GetConnectionString()
            }));
        }

        [Fact]
        public async Task ReadAsync_When_StreamIdDoesntExist_Should_Success_EmptyEvents()
        {
            var streamId = Guid.NewGuid();

            var events = await _eventStore.ReadAsync(streamId);

            events.Should().BeEmpty();
        }
    }
}
