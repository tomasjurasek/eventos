namespace ProcessManager.Controllers
{
    public partial class WebhooksController
    {
        public class PaymentFailed
        {
            public Guid Id { get; set; }
            public string Reason { get; set; }
        }
    }
}