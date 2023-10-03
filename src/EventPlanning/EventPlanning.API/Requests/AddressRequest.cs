namespace EventPlanning.API.Requests
{
    public record AddressRequest
    {
        public required string Street { get; init; }
        public required string StreetNo { get; init; }
        public required string City { get; init; }
        public required string PostalCode { get; init; }
        public required string State { get; init; }
    }
}
