using Polly;
using Polly.Extensions.Http;

namespace ProcessManager
{
    public static class Retry
    {
        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(10));
        }
    }
}
