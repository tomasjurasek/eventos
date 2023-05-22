using Writer.Contracts.Events;

namespace Writer.Application
{
    public static class EventTypeHelper
    {
        public static Type GetType(string type) => type switch
        {
            nameof(EventCreated) => typeof(EventCanceled),
            nameof(EventCanceled) => typeof(EventCanceled),
        };
    }
}
