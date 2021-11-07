using System;

namespace DinnerHall.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public OrderData OrderData { get; set; }
        public DistributionData DistributionData { get; set; }
    }
}