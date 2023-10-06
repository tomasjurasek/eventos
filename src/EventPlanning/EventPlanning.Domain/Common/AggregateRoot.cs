using Dawn;

namespace EventPlanning.Domain.Common
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        protected AggregateRoot(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                Mutate(@event);
            }
        }

        protected AggregateRoot(string id, DateTimeOffset createdAt)
        {
            Id = Guard.Argument(id).NotNull().NotEmpty();
            CreatedAt = createdAt;
        }

        public string Id { get; protected set; }

        public int Version { get; private set; }

        public DateTimeOffset CreatedAt { get; protected set; }

        protected ICollection<IDomainEvent> _uncommitedEvents = new List<IDomainEvent>();
        public IEnumerable<IDomainEvent> UncommitedEvents => _uncommitedEvents;

        protected void Raise(IDomainEvent @event)
        {
            Mutate(@event);
            _uncommitedEvents.Add(@event);
        }

        private void Mutate(IDomainEvent @event)
        {
            Version++;
            ((dynamic)this).On((dynamic)@event);
        }
    }

}