using System.ComponentModel.DataAnnotations;

namespace EventPlanning.API.Requests.CreateEvent
{
    public record CreateEventRequest
    {
        [MaxLength(20)]
        public required string Name { get; init; }

        [MaxLength(100)]
        public required string Description { get; init; }

        public required DateTimeOffset StartedAt { get; init; }

        public required DateTimeOffset FinishedAt { get; init; }

        public required AddressRequest Address { get; init; }

        public required OrganizerRequest Organizer { get; init; }
    }
}
