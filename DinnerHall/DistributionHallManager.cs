using DinnerHall.Models;
using DinnerHall.Singleton;

namespace DinnerHall
{
    public class DistributionHallManager : Singleton<DistributionHallManager>
    {
        public Waiter[] Waiters;
        
        public DistributionHallManager()
        {
            
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