using Dawn;
using EventPlanning.Domain.Common;
using EventPlanning.Domain.Event.Events;
using FluentResults;

namespace EventPlanning.Domain.Event
{
    public class EventAggregate : AggregateRoot
    {
        public static Result<EventAggregate> Create(Guid id, string name, string description, Organizer organizer, Address address, int capacity, DateTimeOffset startedAt, DateTimeOffset finishedAt)
        {
            try
            {
                Guard.Argument(capacity).NotZero().NotNegative();
                Guard.Argument(address).NotNull();
                Guard.Argument(name).NotNull().NotEmpty().MaxLength(20);
                Guard.Argument(description).NotNull().NotEmpty().MaxLength(100);
                Guard.Argument(organizer).NotNull();

                return new EventAggregate(id, name, description, organizer, address, capacity, startedAt, finishedAt);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public EventAggregate() { } // TODO

        internal EventAggregate(Guid id, string name, string description, Organizer organizer, Address address, int capacity, DateTimeOffset startedAt, DateTimeOffset finishedAt) : base(id, DateTimeOffset.UtcNow)
        {
            Raise(new EventCreated { Name = name, Description = description, Address = address, Organizer = organizer, StartedAt = startedAt, FinishedAt = finishedAt, Capacity = capacity });
        }

        public int Capacity { get; private set; }

        public int Registrations { get; private set; }

        public Address Address { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public Organizer Organizer { get; private set; }

        public EventState State { get; private set; }

        public DateTimeOffset StartedAt { get; private set; }
        public DateTimeOffset FinishedAt { get; private set; }

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
                return Result.Fail("EVENT_HAS_REGISTRATIONS");
            }

            State = EventState.Canceled;

            return Result.Ok();
        }


        internal void Apply(EventCreated @event)
        {
            Description = @event.Description;
            Address = @event.Address;
            Name = @event.Name;
        }

    }
}
