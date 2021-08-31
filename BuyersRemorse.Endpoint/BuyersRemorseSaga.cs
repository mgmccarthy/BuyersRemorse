using System;
using System.Threading.Tasks;
using BuyersRemorse.Shared.Messages.Commands;
using BuyersRemorse.Shared.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace BuyersRemorse.Endpoint
{
    public class BuyersRemorseSaga : Saga<BuyersRemorseSaga.SagaData>,
        IAmStartedByMessages<OrderPlaced>,
        IHandleMessages<CancelOrder>,
        IHandleTimeouts<BuyersRemorseSaga.TimeoutState>
    {
        private static readonly ILog Log = LogManager.GetLogger<BuyersRemorseSaga>();

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaData> mapper)
        {
            mapper.ConfigureMapping<OrderPlaced>(message => message.OrderId).ToSaga(sagaData => sagaData.OrderId);
            mapper.ConfigureMapping<CancelOrder>(message => message.OrderId).ToSaga(sagaData => sagaData.OrderId);
        }

        public async Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            Log.Info($"Handling OrderPlaced for OrderId: {message.OrderId}, writing OrderId to Saga Data and setting timeout for 15 seconds");
            Data.OrderId = message.OrderId;
            await RequestTimeout<TimeoutState>(context, TimeSpan.FromSeconds(15));
        }

        public async Task Handle(CancelOrder message, IMessageHandlerContext context)
        {
            Log.Info($"Handling CancelOrder for OrderId: {message.OrderId}, dispatching OrderCancelled and ending the saga");
            await context.Publish<OrderCancelled>(oc =>
            {
                oc.OrderId = Data.OrderId;
            });
            MarkAsComplete();
        }

        public async Task Timeout(TimeoutState state, IMessageHandlerContext context)
        {
            Log.Info($"Buyers remorse period has expired, dispatching ProcessOrder with OrderId: {Data.OrderId} and ending the saga");
            await context.SendLocal(new ProcessOrder{ OrderId = Data.OrderId });
            MarkAsComplete();
        }

        public class SagaData : ContainSagaData
        {
            public Guid OrderId { get; set; }
        }

        public class TimeoutState { }
    }
}
