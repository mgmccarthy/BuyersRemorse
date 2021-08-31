using BuyersRemorse.Shared.Messages.Commands;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace BuyersRemorse.Client.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "BuyersRemorse.Client.ConsoleApp";

            var endpointConfiguration = new EndpointConfiguration("BuyersRemorse.Client.ConsoleApp");
            endpointConfiguration.UsePersistence<LearningPersistence>();

            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            transport.Routing().RouteToEndpoint(messageType: typeof(PlaceOrder), destination: "BuyersRemorse.Endpoint");
            transport.Routing().RouteToEndpoint(messageType: typeof(CancelOrder), destination: "BuyersRemorse.Endpoint");

            endpointConfiguration.SendOnly();

            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            Console.WriteLine("Press 1 to place an Order");
            Console.WriteLine("Press 2 to cancel the Order");
            Console.WriteLine("Press esc to exit");

            Guid orderId = Guid.Empty;

            while (true)
            {
                var result = Console.ReadKey(true);

                if (result.Key == ConsoleKey.D1)
                {
                    orderId = Guid.NewGuid();
                    var placeOrder = new PlaceOrder { OrderId = orderId };
                    Console.WriteLine($"Sending CreateOrder with OrderId: {placeOrder.OrderId}");
                    await endpointInstance.Send(placeOrder).ConfigureAwait(false);
                }

                if (result.Key == ConsoleKey.D2)
                {
                    var cancelOrder = new CancelOrder {OrderId = orderId};
                    Console.WriteLine($"Sending CancelOrder with OrderId: {cancelOrder.OrderId}");
                    await endpointInstance.Send(cancelOrder).ConfigureAwait(false);
                }

                if (result.Key == ConsoleKey.Escape)
                {
                    await endpointInstance.Stop().ConfigureAwait(false);
                }
            }
        }
    }
}