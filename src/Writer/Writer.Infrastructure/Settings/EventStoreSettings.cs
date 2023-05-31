namespace Writer.Infrastructure.Settings
{
    public record class EventStoreSettings
    {
        public string ConnectionString { get; init; }
    }
}
