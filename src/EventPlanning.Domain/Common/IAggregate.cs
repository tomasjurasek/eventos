namespace EventPlanning.Domain.Common
{
    public interface IAggregate
	{
        public string Id { get; }

        public DateTimeOffset CreatedAt { get; }
    }
}
