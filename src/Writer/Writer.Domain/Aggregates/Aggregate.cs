using Domain.Events;

namespace Writer.Domain.Aggregates
{
    public abstract class Aggregate : IAggregate
    {
        protected Aggregate(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                Mutate(@event);
            }
        }

        protected Aggregate() { }


        public Guid Id { get; protected set; }
        public int Version { get; internal set; }
        public DateTimeOffset CreatedAt { get; protected set; }



        protected ICollection<IEvent> _uncommitedEvents = new List<IEvent>();
        public IEnumerable<IEvent> UncommitedEvents => _uncommitedEvents;

        protected void Apply(IEvent @event)
        {
            Mutate(@event);
            _uncommitedEvents.Add(@event);
        }

        private void Mutate(IEvent @event)
        {
            Version++;
            ((dynamic)this).On((dynamic)@event);
        }
    }

    public interface IAggregate
    {
        Guid Id { get; }

        DateTimeOffset CreatedAt { get; }

        int Version { get; }

        IEnumerable<IEvent> UncommitedEvents { get; }
    }
}
