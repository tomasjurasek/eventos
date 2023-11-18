using EventPlanning.Domain.Event;
using FluentAssertions;

namespace EventPlanning.Tests.DomainTests
{
    public class EventAggregateTests
    {

        [Fact]
        public void Create_When_CorrectData_Should_Success()
        {
            var @event = EventAggregate.Create(Id, Name, Description, Organizer, Address, Capacity, StartedAt, FinishedAt);

            @event.IsSuccess.Should().BeTrue();
            @event.Value.Should().NotBeNull();
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void Create_When_InvalidId_Should_Fail(Guid id)
        {
            var @event = EventAggregate.Create(id, Name, Description, Organizer, Address, Capacity, StartedAt, FinishedAt);

            @event.IsFailed.Should().BeTrue();
        }

        [Fact]
        public void Create_When_InvalidOrganizer_Should_Fail()
        {
            var @event = EventAggregate.Create(Id, Name, Description, null, Address, Capacity, StartedAt, FinishedAt);

            @event.IsFailed.Should().BeTrue();
        }


        [Fact]
        public void Create_When_InvalidAddress_Should_Fail()
        {
            var @event = EventAggregate.Create(Id, Name, Description, Organizer, null, Capacity, StartedAt, FinishedAt);

            @event.IsFailed.Should().BeTrue();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Create_When_InvalidCapacity_Should_Fail(int capacity)
        {
            var @event = EventAggregate.Create(Id, Name, Description, Organizer, Address, capacity, StartedAt, FinishedAt);

            @event.IsFailed.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Create_When_InvalidName_Should_Fail(string name)
        {
            var @event = EventAggregate.Create(Id, name, Description, Organizer, Address, Capacity, StartedAt, FinishedAt);

            @event.IsFailed.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Create_When_InvalidDescription_Should_Fail(string description)
        {
            var @event = EventAggregate.Create(Id, Name, description, Organizer, Address, Capacity, StartedAt, FinishedAt);

            @event.IsFailed.Should().BeTrue();
        }

        [Fact]
        public void Cancel_When_EmptyRegistrations_Should_Success()
        {
            var @event = GetRegistration();

            var result = @event.Cancel();

            result.IsSuccess.Should().BeTrue();
            @event.State.Should().Be(EventState.Canceled);
        }

        [Fact]
        public void Cancel_When_CancelState_Should_Fail()
        {
            var @event = GetRegistration();
            @event.Cancel();

            var result = @event.Cancel();

            result.IsFailed.Should().BeTrue();
            @event.State.Should().Be(EventState.Canceled);
        }


        public static Guid Id => Guid.NewGuid();
        public static string Email => "test@test.com";
        public static string Name => "Event";
        public static string Description => "Event Info";
        public static int Capacity => 3;
        public static DateTimeOffset StartedAt => DateTimeOffset.UtcNow;
        public static DateTimeOffset FinishedAt => DateTimeOffset.UtcNow;
        public static Organizer Organizer => new Organizer() { Email = Email };
        public static Address Address => new Address() { City = "Brno", PostalCode = "54345", State = "Czech", Street = "Nova", StreetNo = "1" };

        public EventAggregate GetRegistration() => new EventAggregate(Id, Name, Description, Organizer, Address, Capacity, StartedAt, FinishedAt);
    }
}
