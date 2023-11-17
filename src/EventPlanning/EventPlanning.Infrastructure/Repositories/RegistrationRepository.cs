using EventPlanning.Domain.Event;
using EventPlanning.Domain.Registration;

namespace EventPlanning.Infrastructure.Repositories
{
    internal class RegistrationRepository : IRegistrationRepository
    {
        public Task<RegistrationAggregate> FindAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task StoreAsync(RegistrationAggregate registration)
        {
            throw new NotImplementedException();
        }
    }
}
