using Writer.Contracts.Commands;

namespace ProcessManager.Clients
{
    internal class WriterClient : IWriterClient
    {
        private readonly HttpClient _client;

        public WriterClient(HttpClient client)
        {
            _client = client;
        }

        public async Task Cancel(Guid id, string reason)
        {
            await _client.PostAsJsonAsync<CancelEvent>("events/cancel-event", new CancelEvent { Id = id, Reason = reason });
        }
    }
}
