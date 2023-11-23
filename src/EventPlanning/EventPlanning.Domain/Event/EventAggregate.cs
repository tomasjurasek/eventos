using Dawn;
using EventPlanning.Domain.Event.Events;
using FluentResults;
using Simlife.EventSourcing.Aggregates;

namespace EventPlanning.Domain.Event
{
    public class EventAggregate : AggregateRoot
    {
        public static Result<EventAggregate> Create(Guid id, string name, string description, Organizer organizer, Address address, int capacity, DateTimeOffset startedAt, DateTimeOffset finishedAt)
        {
            try
            {
                Guard.Argument(id).NotDefault();
                Guard.Argument(capacity).NotZero().NotNegative();
                Guard.Argument(address).NotNull();
                Guard.Argument(name).NotNull().NotEmpty();
                Guard.Argument(description).NotNull().NotEmpty();
                Guard.Argument(organizer).NotNull();

                return new EventAggregate(id, name, description, organizer, address, capacity, startedAt, finishedAt);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        internal EventAggregate() { } // Rehydrate?

        internal EventAggregate(Guid id, string name, string description, Organizer organizer, Address address, int capacity, DateTimeOffset startedAt, DateTimeOffset finishedAt)
        {
            Raise(new EventCreated { AggregateId = id.ToString(), Name = name, Description = description, Address = address, Organizer = organizer, StartedAt = startedAt, FinishedAt = finishedAt, Capacity = capacity });
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

            Raise(new EventCanceled { AggregateId = Id.ToString() });

            return Result.Ok();
        }


        // TODO
        public void Apply(EventCreated @event)
        {
            Id = Guid.Parse(@event.AggregateId);
            Name = @event.Name;
            Description = @event.Description;
            Address = @event.Address;
            Organizer = @event.Organizer;
            StartedAt = @event.StartedAt;
            FinishedAt = @event.FinishedAt;
            Capacity = @event.Capacity;
            State = EventState.Open;
        }

        public void Apply(EventCanceled @event)
        {
            Id = Guid.Parse(@event.AggregateId);
            State = EventState.Close;
        }

    }
}
