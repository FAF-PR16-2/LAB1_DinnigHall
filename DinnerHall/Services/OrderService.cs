
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DinnerHall.Models;

namespace DinnerHall.Services
{
    public class OrderService : IOrdersService
    {
        private readonly ConcurrentDictionary<Guid, Order> _orders = DistributionHallManager.GetInstance()._Orders;
        
        public IEnumerable<Order> Get()
        {
            return _orders.Values;
        }
        
        public Order Get(Guid id)
        {
            return _orders.TryGetValue(id, out var order) ? order : null;
        }
        
        public Order Create(Order order)
        {
            var id = Guid.NewGuid();
            order.Id = id;
            if (_orders.TryAdd(id, order))
                return order;
            
            return Create(order);
        }
        
        public Order Update(Guid id, Order order)
        {
            if (!_orders.TryGetValue(id, out var existingOrder))
                return null;
        
            order.Id = id;
            _orders[id] = order;
            return order;
        }
        
        public void Delete(Guid id)
        {
            if (!_orders.TryGetValue(id, out var existingOrder))
                return;

            _orders.TryRemove(id, out var _);
        }

        public void Distribute(Guid id, DistributionData distributionData)
        {
            if (!_orders.TryGetValue(id, out var existingOrder))
                return;
            
            if (!_orders.TryRemove(id, out var deletedOrder))
                return;
            
            DistributionHallManager.GetInstance().ServeOrder(deletedOrder);
        }
        
        
    }
}