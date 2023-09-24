namespace EventPlanning.Domain.Registration
{
    internal class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationService(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }
    }

    public interface IRegistrationService
    {
    }
}
