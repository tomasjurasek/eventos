using Dawn;

namespace EventPlanning.Domain.Common
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        public void Apply(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                Mutate(@event);
            }
        }

        protected AggregateRoot() { }

        protected AggregateRoot(Guid id, DateTimeOffset createdAt)
        {
            Id = Guard.Argument(id).NotDefault();
            CreatedAt = createdAt;
        }

        public Guid Id { get; protected set; }

        public int Version { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }


        protected ICollection<IDomainEvent> _uncommittedEvents = new List<IDomainEvent>();
        public IEnumerable<IDomainEvent> GetUncommittedEvents() => _uncommittedEvents;


        protected void Raise(IDomainEvent @event)
        {
            Mutate(@event);
            _uncommittedEvents.Add(@event);
        }

        private void Mutate(IDomainEvent @event)
        {
            Version++;
            ((dynamic)this).Apply((dynamic)@event);
        }
    }

}