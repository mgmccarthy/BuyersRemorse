using System;
using NServiceBus;

namespace BuyersRemorse.Shared.Messages.Events
{
    // ReSharper disable once InconsistentNaming
    public interface OrderPlaced : IEvent
    {
        Guid OrderId { get; set; }
    }
}
