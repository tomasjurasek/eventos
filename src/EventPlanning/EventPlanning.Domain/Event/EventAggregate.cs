using Dawn;
using EventPlanning.Domain.Common;
using FluentResults;

namespace EventPlanning.Domain.Event
{
    public class EventAggregate : AggregateRoot
    {
        // TODO
        internal EventAggregate(IEnumerable<IDomainEvent> events) : base(events) { }

        internal EventAggregate(Guid id, string name, string description, Organizer organizer, Address address, int capacity) : base(id, DateTimeOffset.UtcNow)
        {
            Capacity = Guard.Argument(capacity).NotZero().NotNegative();
            Address = Guard.Argument(address).NotNull();
            Name = Guard.Argument(name).NotNull().NotEmpty().MaxLength(20);
            Description = Guard.Argument(description).NotNull().NotEmpty().MaxLength(100);
            Organizer = Guard.Argument(organizer).NotNull();
            State = EventState.Created;
        }

        public int Capacity { get; private set; }

        public int Registrations { get; private set; }

        public Address Address { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

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
