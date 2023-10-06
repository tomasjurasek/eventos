namespace EventPlanning.Infrastructure.Options
{
    public record class EventStoreOptions
    {
        public required string ConnectionString { get; init; }
    }
}
