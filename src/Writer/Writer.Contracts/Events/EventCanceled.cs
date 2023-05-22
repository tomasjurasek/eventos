﻿namespace Writer.Contracts.Events
{
    public record EventCanceled : Event
    {
        public required DateTimeOffset CanceledAt { get; init; }
    }
}