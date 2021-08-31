using System.Threading.Tasks;
using BuyersRemorse.Shared.Messages.Commands;
using NServiceBus;
using NServiceBus.Logging;

namespace BuyersRemorse.Endpoint
{
    public class ProcessOrderHandler : IHandleMessages<ProcessOrder>
    {
        private static readonly ILog Log = LogManager.GetLogger<ProcessOrderHandler>();

        public Task Handle(ProcessOrder message, IMessageHandlerContext context)
        {
            Log.Info($"Handling ProcessOrder with OrderId: {message.OrderId}");
            return Task.CompletedTask;
        }
    }
}
