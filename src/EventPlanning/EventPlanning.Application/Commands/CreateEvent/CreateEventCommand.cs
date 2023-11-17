﻿using EventPlanning.Domain.Event;
using System.ComponentModel.DataAnnotations;

namespace EventPlanning.Application.Commands.CreateEvent
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
