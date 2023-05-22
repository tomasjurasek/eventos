namespace Reader.API.DTO
{
    public class EventDTO
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? CanceledAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
