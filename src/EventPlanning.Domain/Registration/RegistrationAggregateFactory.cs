using FluentResults;

namespace EventPlanning.Domain.Registration
{
    internal class RegistrationAggregateFactory : IRegistrationAggregateFactory
    {
        public Result<RegistrationAggregate> Create(string id, string eventId, Attendee attendee)
        {
            try
            {
                var registration = new RegistrationAggregate(id, eventId, attendee);
                return Result.Ok(registration);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }

    public interface IRegistrationAggregateFactory
    {
        Result<RegistrationAggregate> Create(string id, string eventId, Attendee attendee);
    }
}
