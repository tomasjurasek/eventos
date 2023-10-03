namespace EventPlanning.Domain.Registration
{
    public interface IRegistrationRepository
    {
        Task<RegistrationAggregate> FindAsync(string Id);
        Task StoreAsync(RegistrationAggregate registration);
    }
}
