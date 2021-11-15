using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DinnerHall
{
    public class Waiter
    {
        public int  Id => _id;

        private int _id;
        
        private Table[] _tables;
        private Thread _thread;

        private List<DistributionData> _ordersToServe;
        // private Mutex _mutex;
        
        public Waiter(int id, Table[] tables)
        {
            _id = id;
            _tables = tables;

            _ordersToServe = new List<DistributionData>();
            
            _thread = new Thread(Update);
        }

        public void ServeOrder(DistributionData distributionData)
        {
            _ordersToServe.Add(distributionData);
        }

        public void Start() //start roaming
        {
            _thread.Start();
            
            //_mutex = new Mutex();
        }

        private void Update()
        {
            while (true)
            {
                foreach (var table in _tables)
                {
         
                    if (table.Status == Table.TableStatus.MakingOrder)
                    {
                        if (!table.MakeOrder())
                        {
                            continue;
                        }

                        var random = new Random();
                        
                        Thread.Sleep(random.Next(Configuration.TimeMinWaitOrder,
                            Configuration.TimeMaxWaitOrder));
                        
                        var orderData = table.CurrentOrderData;

                        orderData.waiter_id = Id;
                        orderData.pick_up_time = DateTimeOffset.Now.ToUnixTimeSeconds();
                        
                        TrySendRequest(orderData);
                        DistributionHallManager.GetInstance().AddOrderDataToList(orderData);
                    }

                    if (table.Status == Table.TableStatus.WaitingForOrder)
                    {
                        var ordersToServe = new List<DistributionData>(_ordersToServe); 
                        
                        foreach (var order in ordersToServe)
                        {
                            if (order.table_id == table.Id)
                            {
                                table.ReceiveOrder(order);
                                _ordersToServe.Remove(order);
                            }
                        }
                    }
                }
            }
        }
        
        private void TrySendRequest(OrderData orderData)
        {
            try
            {
                RequestsSender.SendOrderRequest(orderData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}