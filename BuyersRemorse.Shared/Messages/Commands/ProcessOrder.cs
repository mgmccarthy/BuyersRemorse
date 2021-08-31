using System;
using NServiceBus;

namespace BuyersRemorse.Shared.Messages.Commands
{
    public class ProcessOrder : ICommand
    {
        public Guid OrderId { get; set; }
    }
}
