using Dawn;

namespace EventPlanning.Domain.Common
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        public void Apply(IList<IDomainEvent> events)
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

        public long Version { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }


        private IList<IDomainEvent> _uncommittedEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> GetUncommittedEvents() => _uncommittedEvents.AsReadOnly();


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