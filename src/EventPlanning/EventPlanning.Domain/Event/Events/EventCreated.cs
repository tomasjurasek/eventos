﻿using Simplife.Core.Events;

namespace EventPlanning.Domain.Event.Events
{
    public record EventCreated : IEvent
    {
        public string AggregateId { get; init; }
        public required string Name { get; init; }
        public required int Capacity { get; init; }
        public required string Description { get; init; }
        public required DateTimeOffset StartedAt { get; init; }
        public required DateTimeOffset FinishedAt { get; init; }
        public required Address Address { get; init; }
        public required Organizer Organizer { get; init; }
        public required bool AutoConfirmRegistrations { get; init; }

    }
}
