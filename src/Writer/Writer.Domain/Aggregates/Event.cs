using Dawn;
using FluentResults;
using Writer.Contracts.Common;
using Writer.Contracts.Events;
using Writer.Domain.Aggregates.Base;

namespace Writer.Domain.Aggregates
{
    public class Event : Aggregate
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Capacity { get; private set; }
        public DateTimeOffset StartedAt { get; private set; }
        public DateTimeOffset FinishedAt { get; private set; }
        public DateTimeOffset? CanceledAt { get; private set; }
        public Organizer Organizer { get; private set; }
        public Location Location { get; private set; }

        public Event(IEnumerable<IEvent> events) : base(events) { }

        public Event(
            string name,
            string description,
            DateTimeOffset startedAt,
            DateTimeOffset finishedAt,
            Location location,
            Organizer organizer) : base()
        {

            Guard.Argument(name, nameof(name)).NotEmpty().NotNull();
            Guard.Argument(description, nameof(description)).NotEmpty().NotNull();
            Guard.Argument(location, nameof(location)).NotNull();
            Guard.Argument(organizer, nameof(organizer)).NotNull();
            Guard.Argument(startedAt, nameof(startedAt)).Min(DateTimeOffset.UtcNow);
            Guard.Argument(finishedAt, nameof(finishedAt)).Min(startedAt);


            Apply(new EventCreated
            {
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                StartedAt = startedAt,
                FinishedAt = finishedAt,
                Organizer = organizer,
                Location = location,
            });
        }

        public Result Cancel()
        {
            if (StartedAt >= DateTimeOffset.UtcNow)
            {
                return Result.Fail("Event alredy started. You cannot cancel it.");
            }


            Apply(new EventCanceled
            {
                CreatedAt = DateTimeOffset.UtcNow,
                Id = Id,
                CanceledAt = DateTimeOffset.UtcNow
            });

            return Result.Ok();
        }

        public Result ChangeLocation(Location location)
        {
            if (location is null)
            {
                return Result.Fail("Location cannot be null.");
            }

            if (StartedAt >= DateTimeOffset.UtcNow)
            {
                return Result.Fail("Event alredy started. You cannot change the location");
            }

            Apply(new EventLocationChanged
            {
                Location = location,
                Id = Id,
                CreatedAt = DateTimeOffset.UtcNow
            });

            return Result.Ok();
        }


        private void On(EventLocationChanged @event)
        {
            Location = @event.Location;
        }

        private void On(EventCanceled @event)
        {
            CreatedAt = @event.CreatedAt;
            CanceledAt = @event.CanceledAt;
        }

        private void On(EventCreated @event)
        {
            Id = @event.Id;
            CreatedAt = @event.CreatedAt;
            Name = @event.Name;
            Description = @event.Description;
            StartedAt = @event.StartedAt;
            FinishedAt = @event.FinishedAt;
            Organizer = @event.Organizer;
            Location = @event.Location;
        }
    }
}
