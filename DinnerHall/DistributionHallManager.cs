using DinnerHall.Singleton;

namespace DinnerHall
{
    public class DistributionHallManager : Singleton<DistributionHallManager>
    {
        public Waiter[] Waiters;
        
        public DistributionHallManager()
        {
            
        }

        public void ServeOrder(DistributionData distributionData)
        {
            foreach (var waiter in Waiters)
            {
                if (waiter.Id == distributionData.waiter_id)
                {
                    waiter.ServeOrder(distributionData);
                }
            }
        }
    }
}