using EventPlanning.Domain.Common;
using EventPlanning.Domain.Event;
using FluentResults;

namespace EventPlanning.Application.Commands.CreateEvent
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
                { IsSuccess: true } => await StoreAsync(result),
                _ => result.ToResult()
            };

            async Task<Result<CommandResult>> StoreAsync(Result<EventAggregate> result)
            {
                var storeResult = await _eventRepository.StoreAsync(result.Value);

                return storeResult switch
                {
                    { IsSuccess: true } => Result.Ok(new CommandResult
                    {
                        Id = result.Value.Id
                    }),
                    _ => storeResult
                };
            }
        }
    }
}
