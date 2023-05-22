namespace Writer.Contracts.Events
{
    public interface IEvent
    {
        Guid Id { get; }

        DateTimeOffset CreatedAt { get; }
    }
}
