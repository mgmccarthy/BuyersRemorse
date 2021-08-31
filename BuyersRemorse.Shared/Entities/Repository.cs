using System.Collections.Generic;

namespace BuyersRemorse.Shared.Entities
{
    public static class Repository
    {
        private static readonly List<Order> orders= new List<Order>();

        public static void CreateOrder(Order order)
        {
            orders.Add(order);
        }
    }
}
