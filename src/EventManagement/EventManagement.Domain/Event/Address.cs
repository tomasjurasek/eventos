namespace EventManagement.Domain.Event
{
    public record Address
    {
        public required string Street { get; init; }
        public required string StreetNo { get; init; }
        public required string City { get; init; }
        public required string PostalCode { get; init; }
        public required string State { get; init; }
    }
}
