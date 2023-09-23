using FluentResults;

namespace EventPlanning.Domain.Registration
{
    internal class RegistrationAggregateFactory : IRegistrationAggregateFactory
    {
        public Result<RegistrationAggregate> Create(string id, Attendee attendee)
        {
            try
            {
                var registration = new RegistrationAggregate(id, attendee);
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
        Result<RegistrationAggregate> Create(string id, Attendee attendee);
    }
}
