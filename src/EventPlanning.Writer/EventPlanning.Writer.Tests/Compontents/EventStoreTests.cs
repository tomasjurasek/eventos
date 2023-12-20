using FluentAssertions;
using Microsoft.Extensions.Options;
using Testcontainers.EventStoreDb;

namespace EventPlanning.Writer.Tests.Compontents
{
    public class EventStoreTests
    {
        //private readonly Infrastructure.Stores.EventStore _eventStore;
        //public EventStoreTests()
        //{
        //    var container = new EventStoreDbBuilder().Build();
        //    container.StartAsync().Wait(); // IAsyncLifetime or some Init

        //    _eventStore = new Infrastructure.Stores.EventStore(Options.Create(new Infrastructure.Options.EventStoreOptions
        //    {
        //        ConnectionString = container.GetConnectionString()
        //    }));
        //}

        //[Fact]
        //public async Task ReadAsync_When_StreamIdDoesntExist_Should_Success_EmptyEvents()
        //{
        //    var streamId = Guid.NewGuid();

        //    var events = await _eventStore.ReadAsync(streamId);

        //    events.Should().BeEmpty();
        //}

        //[Fact(Skip ="TODO")]
        //public async Task StoreAsync_When_ConcurrencyStreamVersionIsSame_Should_Fail()
        //{
        //    var streamId = Guid.NewGuid();
        //    var events = new List<IDomainEvent> // TODO
        //    { 
        //        new EventCreated(){ Address = default, Description = default, FinishedAt= default, Name = default, Organizer = default, StartedAt= default, Capacity = default}
        //    };

        //    var firstResult = await _eventStore.StoreAsync(streamId, 1, events);
        //    var secondResult  = await _eventStore.StoreAsync(streamId, 1, events);

        //    firstResult.IsSuccess.Should().BeTrue();
        //    secondResult.IsFailed.Should().BeTrue();
        //}
    }
}
