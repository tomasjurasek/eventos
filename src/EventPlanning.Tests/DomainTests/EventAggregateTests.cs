using EventPlanning.Domain.Event;
using FluentAssertions;

namespace EventPlanning.Tests.DomainTests
{
    public class EventAggregateTests
    {

        private readonly EventAggregateFactory _eventAggregateFactory;

        public EventAggregateTests()
        {
            _eventAggregateFactory = new EventAggregateFactory();
        }

        [Fact]
        public void Create_When_CorrectData_Should_Success()
        {
            var @event = _eventAggregateFactory.Create(Id, Organizer, Address, Capacity);

            @event.IsSuccess.Should().BeTrue();
            @event.Value.Should().NotBeNull();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Create_When_InvalidId_Should_Fail(string id)
        {
            var @event = _eventAggregateFactory.Create(id, Organizer, Address, Capacity);

            @event.IsFailed.Should().BeTrue();
        }

        [Fact]
        public void Create_When_InvalidOrganizer_Should_Fail()
        {
            var @event = _eventAggregateFactory.Create(Id, null, Address, Capacity);

            @event.IsFailed.Should().BeTrue();
        }


        [Fact]
        public void Create_When_InvalidAddress_Should_Fail()
        {
            var @event = _eventAggregateFactory.Create(Id, Organizer, null, Capacity);

            @event.IsFailed.Should().BeTrue();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Create_When_InvalidCapacity_Should_Fail(int capacity)
        {
            var @event = _eventAggregateFactory.Create(Id, Organizer, Address, capacity);

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


        public static string Id => Guid.NewGuid().ToString();
        public static string Email => "test@test.com";
        public static int Capacity => 3;
        public static Organizer Organizer => new Organizer() { Email = Email };
        public static Address Address => new Address() { City = "Brno", PostalCode = "54345", State = "Czech", Street = "Nova", StreetNo = "1" };

        public EventAggregate GetRegistration() => new EventAggregate(Id, Organizer, Address, Capacity);
    }
}
