using EventPlanning.Domain.Common;
using EventPlanning.Domain.Event;
using FluentResults;

namespace EventPlanning.Application.Commands
{
    public class CreateEventCommandHandler : CommandHandlerBase<CreateEventCommand>
    {
        private readonly IAggregateRootRepository<EventAggregate> _eventRepository;

        public CreateEventCommandHandler(IAggregateRootRepository<EventAggregate> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        protected override async Task<Result<CommandResult>> HandleAsync(CreateEventCommand command)
        {
            var result = EventAggregate.Create(Guid.NewGuid(), command.Name, command.Description, command.Organizer, command.Address, command.Capacity, command.StartedAt, command.FinishedAt);
            return result switch
            {
                { IsSuccess: true } => await StoreAsync(result.Value),
                _ => result.ToResult()
            };

            async Task<Result<CommandResult>> StoreAsync(EventAggregate aggregate)
            {
                var result = await _eventRepository.StoreAsync(aggregate);

                return result switch
                {
                    { IsSuccess: true } => Result.Ok(new CommandResult
                    {
                        Id = aggregate.Id
                    }),
                    _ => result
                };
            }
        }
    }
}
