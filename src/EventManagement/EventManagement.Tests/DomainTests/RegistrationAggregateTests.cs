using EventManagement.Domain.Registration;
using FluentAssertions;

namespace EventManagement.Tests.DomainTests
{
    public class RegistrationAggregateTests
    {

        //[Fact]
        //public void Create_When_CorrectData_Should_Success()
        //{
        //    var registration = _registrationAggregateFactory.Create(Id, EventId, Attendee);

        //    registration.IsSuccess.Should().BeTrue();
        //    registration.Value.Should().NotBeNull();
        //}

        //[Theory]
        //[InlineData("00000000-0000-0000-0000-000000000000")]
        //public void Create_When_InvalidId_Should_Fail(Guid id)
        //{
        //    var registration = _registrationAggregateFactory.Create(id, EventId, Attendee);

        //    registration.IsFailed.Should().BeTrue();
        //}

        //[Theory]
        //[InlineData("00000000-0000-0000-0000-000000000000")]
        //public void Create_When_InvalidEventId_Should_Fail(Guid eventId)
        //{
        //    var registration = _registrationAggregateFactory.Create(Id, eventId, Attendee);

        //    registration.IsFailed.Should().BeTrue();
        //}

        //[Fact]
        //public void Create_When_InvalidAttendee_Should_Fail()
        //{
        //    var registration = _registrationAggregateFactory.Create(Id, EventId, null!);

        //    registration.IsFailed.Should().BeTrue();
        //}

        [Fact]
        public void Accept_When_WaitingState_Should_Success()
        {
            var registration = GetRegistration();
            var result = registration.Accept();

            result.IsSuccess.Should().BeTrue();
            registration.State.Should().Be(RegistrationState.Accepted);
        }

        [Fact]
        public void Accept_When_DeclineState_Should_Fail()
        {
            var registration = GetRegistration();
            registration.Decline();

            var result = registration.Accept();

            result.IsFailed.Should().BeTrue();
            registration.State.Should().Be(RegistrationState.Declined);
        }

        [Fact]
        public void Decline_When_DeclineState_Should_Fail()
        {
            var registration = GetRegistration();
            registration.Decline();

            var result = registration.Decline();

            result.IsFailed.Should().BeTrue();
            registration.State.Should().Be(RegistrationState.Declined);
        }

        [Fact]
        public void Decline_When_WaitingState_Should_Success()
        {
            var registration = GetRegistration();
            var result = registration.Decline();

            result.IsSuccess.Should().BeTrue();
            registration.State.Should().Be(RegistrationState.Declined);
        }

        [Fact]
        public void Decline_When_AcceptState_Should_Fail()
        {
            var registration = GetRegistration();
            registration.Accept();

            var result = registration.Decline();

            result.IsFailed.Should().BeTrue();
            registration.State.Should().Be(RegistrationState.Accepted);
        }

        [Fact]
        public void Cancel_When_EventStartsLessThan4Hours_Should_Fail()
        {
            var registration = GetRegistration();

            var result = registration.Cancel(DateTimeOffset.UtcNow.AddHours(3), Email);

            result.IsFailed.Should().BeTrue();
            registration.State.Should().Be(RegistrationState.Waiting);
        }

        [Fact]
        public void Cancel_When_EventStartsLessThan4Hours_IncorrectUserEmail_Should_Fail()
        {
            var registration = GetRegistration();

            var result = registration.Cancel(DateTimeOffset.UtcNow.AddHours(3), "other@other.com");

            result.IsFailed.Should().BeTrue();
            registration.State.Should().Be(RegistrationState.Waiting);
        }

        [Fact]
        public void Cancel_When_EventStartsMoreThan4Hours_Should_Fail()
        {
            var registration = GetRegistration();

            var result = registration.Cancel(DateTimeOffset.UtcNow.AddHours(5), Email);

            result.IsSuccess.Should().BeTrue();
            registration.State.Should().Be(RegistrationState.Canceled);
        }

        [Fact]
        public void Cancel_When_EventStartsMoreThan4Hours_IncorrectUserEmail_Should_Fail()
        {
            var registration = GetRegistration();

            var result = registration.Cancel(DateTimeOffset.UtcNow.AddHours(5), "other@other.com");

            result.IsFailed.Should().BeTrue();
            registration.State.Should().Be(RegistrationState.Waiting);
        }


        public static Guid Id => Guid.NewGuid();
        public static Guid EventId => Guid.NewGuid();
        public static string Email => "test@test.com";
        public static Attendee Attendee => new Attendee() { Email = Email };

        public RegistrationAggregate GetRegistration() => new RegistrationAggregate(Id, EventId, Attendee);
    }
}
