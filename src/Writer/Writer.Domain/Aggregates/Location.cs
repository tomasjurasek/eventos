namespace Writer.Domain.Aggregates
{
    public record Location
    {
        public required string City { get; init; }
        public required string Street { get; init; }
        public required string StreetNo { get; init; }
        public required string ZipCode { get; init; }
    }
}
