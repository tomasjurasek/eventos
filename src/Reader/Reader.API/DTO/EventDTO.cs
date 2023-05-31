namespace Reader.API.DTO
{
    public record EventDTO
    {
        public string Name { get; init; }
        public DateTimeOffset StartedAt { get; init; }
        public DateTimeOffset FinishedAt { get; init; }
        public bool IsCanceled { get; init; }
    }
}
