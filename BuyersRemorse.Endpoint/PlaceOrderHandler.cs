using System.Threading.Tasks;
using BuyersRemorse.Shared.Entities;
using BuyersRemorse.Shared.Messages.Commands;
using BuyersRemorse.Shared.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace BuyersRemorse.Endpoint
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        private static readonly ILog Log = LogManager.GetLogger<PlaceOrderHandler>();

        public Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            Log.Info($"Handling PlaceOrder with OrderId: {message.OrderId}, persisting the order and publishing OrderPlaced");
            Repository.CreateOrder(new Order { OrderId =  message.OrderId });
            return context.Publish<OrderPlaced>(oc =>
            {
                oc.OrderId = message.OrderId;
            });
        }
    }
}
