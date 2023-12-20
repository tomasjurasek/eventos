using FluentResults;
using Simplife.EventSourcing.Aggregates;

namespace EventPlanning.Writer.Domain.Common
{
    public interface IAggregateRootRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        Task<Result<TAggregate>> FindAsync(Guid Id);
        Task<Result> StoreAsync(TAggregate aggregate);
    }
}
