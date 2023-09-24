using EventPlanning.Domain.Registration;
using FluentAssertions;

namespace EventPlanning.Tests.DomainTests
{
    public class RegistrationAggregateTests
    {
        private readonly RegistrationAggregateFactory _registrationAggregateFactory;

        public RegistrationAggregateTests()
        {
            _registrationAggregateFactory = new RegistrationAggregateFactory();
        }

        [Fact]
        public void Create_When_CorrectData_Should_Success()
        {
            var registration = _registrationAggregateFactory.Create(Id, Attendee);

            registration.IsSuccess.Should().BeTrue();
            registration.Value.Should().NotBeNull();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Create_When_InvalidId_Should_Fail(string id)
        {
            var registration = _registrationAggregateFactory.Create(id, Attendee);

            registration.IsFailed.Should().BeTrue();
        }

        [Fact]
        public void Create_When_InvalidAttendee_Should_Fail()
        {
            var registration = _registrationAggregateFactory.Create(Id, null);

            registration.IsFailed.Should().BeTrue();
        }

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
        public void Decline_When_AcceptState_Should_Failt()
        {
            var registration = GetRegistration();
            registration.Accept();

            var result = registration.Decline();

            result.IsFailed.Should().BeTrue();
            registration.State.Should().Be(RegistrationState.Accepted);
        }


        public static string Id => Guid.NewGuid().ToString();
        public static Attendee Attendee => new Attendee();

        public RegistrationAggregate GetRegistration() => new RegistrationAggregate(Id, Attendee);
    }
}
