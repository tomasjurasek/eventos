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
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

            return Result.Ok();
        }
    }

    public interface IRegistrationAggregateFactory
    {
        Result<RegistrationAggregate> Create(string id, Attendee attendee);
    }
}
