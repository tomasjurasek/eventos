using Dawn;
using EventPlanning.Domain.Common;
using FluentResults;

namespace EventPlanning.Domain.Event
{
    public class EventAggregate : IAggregateRoot
    {
        internal EventAggregate(string id, Organizer organizer, Address address, int capacity)
        {
            Id = Guard.Argument(id).NotNull().NotEmpty();
            Capacity = Guard.Argument(capacity).NotZero().NotNegative();
            Address = Guard.Argument(address).NotNull();
            Organizer = Guard.Argument(organizer).NotNull();
            CreatedAt = DateTimeOffset.UtcNow;
            State = EventState.Created;
        }

        public string Id { get; }

        public int Capacity { get; private set; }

        public int Registrations { get; private set; }

        public Address Address { get; private set; }

        public DateTimeOffset CreatedAt { get; }

        public Organizer Organizer { get; private set; }

        public EventState State { get; private set; }

        public Result AddRegistration()
        {
            if (Capacity >= Registrations)
            {
                return Result.Fail("EVENT_IS_FULL");
            }

            Registrations += 1;

            return Result.Ok();
        }

        public Result Cancel()
        {
            if (State == EventState.Canceled)
            {
                return Result.Fail("EVENT_IS_ALREADY_CANCELED");
            }

            if (Registrations > 0)
            {
                return Result.Fail("EVENT_HAS_REGISTRATION");
            }

            State = EventState.Canceled;

            return Result.Ok();
        }

    }
}
