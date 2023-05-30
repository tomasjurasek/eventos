using Writer.Domain.Aggregates;

namespace Writer.Application.Mappers
{
    internal static class LocationMapping
    {
        public static Location Map(this Handlers.CreateEvent.Location location) => new Location
        {
            City = location.City,
            Street = location.Street,
            StreetNo = location.StreetNo,
            ZipCode = location.ZipCode,
        };

    }
}
