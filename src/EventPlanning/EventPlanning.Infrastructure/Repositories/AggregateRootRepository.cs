using EventPlanning.Domain.Common;
using FluentResults;

namespace EventPlanning.Infrastructure.Repositories
{
    internal class AggregateRootRepository<TAggregate> : IAggregateRootRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        public Task<Result<TAggregate>> FindAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> StoreAsync(TAggregate aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
