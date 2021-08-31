using System;
using NServiceBus;

namespace BuyersRemorse.Shared.Messages.Commands
{
    public class CancelOrder : ICommand
    {
        public Guid OrderId { get; set; }
    }
}