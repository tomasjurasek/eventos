namespace ProcessManager.Clients
{
    public interface IWriterClient
    {
        Task Cancel(Guid id, string reason);
    }
}