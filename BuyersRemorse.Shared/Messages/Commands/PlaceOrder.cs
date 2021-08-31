using System;
using NServiceBus;

namespace BuyersRemorse.Shared.Messages.Commands
{
    public class PlaceOrder : ICommand
    {
        public Guid OrderId { get; set; }
    }
}
