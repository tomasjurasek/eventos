using EventManagement.Domain.Event;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Application.Commands
{
    public record CreateEventCommand : ICommand
    {
        public required string Name { get; init; }
        public required string Description { get; init; }
        public DateTimeOffset StartedAt { get; init; }
        public DateTimeOffset FinishedAt { get; init; }

        [Range(0, int.MaxValue)]
        public int Capacity { get; init; }
        public required Organizer Organizer { get; init; }
        public required Address Address { get; init; }
    }
}
