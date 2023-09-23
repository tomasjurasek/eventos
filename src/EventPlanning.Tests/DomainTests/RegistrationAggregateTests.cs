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


        public static string Id => Guid.NewGuid().ToString();
        public static Attendee Attendee => new Attendee();
    }
}
