using FluentResults;
using MassTransit;
using System.ComponentModel.DataAnnotations;
using Writer.Application.Mappers;
using Writer.Domain.Aggregates;
using Writer.Domain.Repositories;

namespace Writer.Application.Handlers.CreateEvent
{
    public record class CreateEventCommand
    {
        public required string Organizer { get; init; }
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required int Capacity { get; init; }
        public required DateTimeOffset StartedAt { get; init; }
        public required DateTimeOffset FinishedAt { get; init; }
        public required Location Location { get; init; }
    }

    public record class Location
    {
        public required string City { get; init; }
        public required string Street { get; init; }
        public required string StreetNo { get; init; }
        public required string ZipCode { get; init; }
    }

    public class CreateEventCommandHandler : CommandHandler<CreateEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        public CreateEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        protected override async Task<Result> ConsumeAsync(CreateEventCommand command)
        {
            try
            {
                var aggregate = new Domain.Aggregates.Event(
               command.Name,
               command.Description,
               command.StartedAt,
               command.FinishedAt,
               command.Location.Map(),
               new Organizer
               {
                   Email = command.Organizer
               });

                await _eventRepository.StoreAsync(aggregate);

            }
            catch (ArgumentException ex)
            {
                return Result.Fail(ex.Message);
            }

            return Result.Ok();
        }
    }
}

