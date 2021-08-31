using System;
using NServiceBus;

namespace BuyersRemorse.Shared.Messages.Events
{
    // ReSharper disable once InconsistentNaming
    public interface OrderCancelled : IEvent
    {
        Guid OrderId { get; set; }
    }
}
