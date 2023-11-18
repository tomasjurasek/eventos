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

        public EventAggregate() { } // TODO Fix

        internal EventAggregate(Guid id, string name, string description, Organizer organizer, Address address, int capacity, DateTimeOffset startedAt, DateTimeOffset finishedAt) : base(id, DateTimeOffset.UtcNow)
        {
            Raise(new EventCreated { Id = id, Name = name, Description = description, Address = address, Organizer = organizer, StartedAt = startedAt, FinishedAt = finishedAt, Capacity = capacity });
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
            if (State == EventState.Close)
            {
                return Result.Fail("EVENT_IS_ALREADY_CANCELED");
            }

            if (Registrations > 0)
            {
                return Result.Fail("EVENT_HAS_REGISTRATIONS");
            }

            Raise(new EventCanceled { Id = Id });

            return Result.Ok();
        }


        internal void Apply(EventCreated @event)
        {
            Id = @event.Id;
            Name = @event.Name;
            Description = @event.Description;
            Address = @event.Address;
            Organizer = @event.Organizer;
            StartedAt = @event.StartedAt;
            FinishedAt = @event.FinishedAt;
            Capacity = @event.Capacity;
            State = EventState.Open;
        }

        internal void Apply(EventCanceled @event)
        {
            Id = @event.Id;
            State = EventState.Close;
        }

    }
}
