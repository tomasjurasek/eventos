namespace Domain.Events.Models
{
    public record Organizer()
    {
        public required string Email { get; init; }
    }
}
