namespace EventPlanning.Domain.Common
{
    public interface IAggregateRoot
    {
        Guid Id { get; }

        DateTimeOffset CreatedAt { get; }

        long Version { get; }

        IReadOnlyList<IDomainEvent> GetUncommittedEvents();

        void Apply(IList<IDomainEvent> events);

    }
}
