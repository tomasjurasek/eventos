namespace Writer.Domain.Aggregates
{
    public record Organizer ()
    {
        public required string Email { get; init; }
    }
}
