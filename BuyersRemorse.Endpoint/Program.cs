using NServiceBus;
using System;
using System.Threading.Tasks;

namespace BuyersRemorse.Endpoint
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "BuyersRemorse.Endpoint";

            var endpointConfiguration = new EndpointConfiguration("BuyersRemorse.Endpoint");
            endpointConfiguration.UsePersistence<LearningPersistence>();
            endpointConfiguration.UseTransport<LearningTransport>();

            var endpointInstance = await NServiceBus.Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            //don't need seed for this operation
            //ScheduledTaskRepo.Seed(); //will create ReminderSchedule and PatientRecord

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
