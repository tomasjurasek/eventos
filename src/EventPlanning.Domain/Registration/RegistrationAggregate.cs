using Dawn;
using EventPlanning.Domain.Common;
using FluentResults;

namespace EventPlanning.Domain.Registration
{
    public class RegistrationAggregate : IAggregate
    {
        internal RegistrationAggregate(string id, Attendee attendee)
        {
            Id = Guard.Argument(id).NotNull().NotEmpty(); // Replace
            Attendee = Guard.Argument(attendee).NotNull();
            CreatedAt = DateTimeOffset.UtcNow;
            State = RegistrationState.Waiting;
        }

        public string Id { get; }

        public DateTimeOffset CreatedAt { get; }

        public Attendee Attendee { get; private set; }

        public RegistrationState State { get; private set; }


        public Result Accept()
        {
            if (State == RegistrationState.Waiting)
            {
                State = RegistrationState.Accepted;
            }
            else
            {
                return Result.Fail("Registration is not in a waiting state.");
            }

            return Result.Ok();
        }

        public Result Decline()
        {
            if (State == RegistrationState.Waiting)
            {
                State = RegistrationState.Declined;
            }
            else
            {
                return Result.Fail("Registration is not in a waiting state.");
            }

            return Result.Ok();
        }

    }
}
