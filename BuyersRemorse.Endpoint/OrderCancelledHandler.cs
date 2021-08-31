using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BuyersRemorse.Shared.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace BuyersRemorse.Endpoint
{
    public class OrderCancelledHandler : IHandleMessages<OrderCancelled>
    {
        private static readonly ILog Log = LogManager.GetLogger<OrderCancelledHandler>();

        public Task Handle(OrderCancelled message, IMessageHandlerContext context)
        {
            Log.Info($"Handling OrderCancelled with OrderId: {message.OrderId}");
            return Task.CompletedTask;
        }
    }
}
