using System;
using System.Collections.Concurrent;
using DinnerHall.Models;
using DinnerHall.Singleton;

namespace DinnerHall
{
    public class DistributionHallManager : Singleton<DistributionHallManager>
    {
        public Waiter[] Waiters;
        public readonly ConcurrentDictionary<Guid, Order> _Orders;
        
        public DistributionHallManager()
        {
            _Orders = new();
        }

        public void AddOrderToList(Order order)
        {
            var id = Guid.NewGuid();
            order.Id = id;
            if (_Orders.TryAdd(id, order))
                return;
            
            AddOrderToList(order);
        }
        
        public void AddOrderDataToList(OrderData orderData)
        {
            AddOrderToList(new Order {OrderData = orderData});
        }

        public void ServeOrder(Order order)
        {
            foreach (var waiter in Waiters)
            {
                if (waiter.Id == order.DistributionData.waiter_id)
                {
                    waiter.ServeOrder(order.DistributionData);
                }
            }
        }
    }
}