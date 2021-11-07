using System;
using System.Collections.Generic;
using DinnerHall.Models;

namespace DinnerHall.Services
{
    public interface IOrdersService
    {
        IEnumerable<Order> Get();
        Order Get(Guid id);
        Order Create(Order order);
        Order Update(Guid id, Order order);
        void Delete(Guid id);

        void Distribute(Guid id, DistributionData distributionData);
    }
}