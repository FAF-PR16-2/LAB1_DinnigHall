using System;
using System.Threading;
using System.Threading.Tasks;

namespace DinnerHall
{
    public class RandomOrdersGenerator
    {
        private int _lastOrderId;
        private int[] _tablesId;
        private int[] _waitersId;
        private int[] _priorities;

        private ItemsBuilder _itemsBuilder;

        private ItemData[] possibleItemsData;

        public Task StartAsync()
        {
            _lastOrderId = 0;
            _tablesId = new []{0, 1, 2, 3};
            _waitersId = new []{0, 1};
            _priorities = new []{1, 2, 3, 4, 5};

            _itemsBuilder = new ItemsBuilder();
            possibleItemsData = _itemsBuilder.GetItems();

            while (true)
            {
                var orderData = OrderDataGenerator();
                Console.WriteLine("------------\n" + orderData.ToString());
                TrySendRequest(orderData);
                
                Thread.Sleep(20000);
            }

            
        }

        private void TrySendRequest(OrderData orderData)
        {
            try
            {
                RequestsSenderClient.SendOrderRequest(orderData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private OrderData OrderDataGenerator()
        {
            var random = new Random();

            var orderData = new OrderData
            {
                order_id = _lastOrderId++,
                table_id = _tablesId[random.Next(_tablesId.Length)],
                waiter_id = _waitersId[random.Next(_waitersId.Length)],
                items = new int[random.Next(1, 5)],
                priority = _priorities[random.Next(_priorities.Length)],
                pick_up_time = DateTimeOffset.Now.ToUnixTimeSeconds()
            };

            for (var i = 0; i < orderData.items.Length; i++)
            {
                orderData.items[i] = possibleItemsData[random.Next(possibleItemsData.Length)].id;
            }

            
            int maxPreparationTime = _itemsBuilder.GetItemDataByItemId(orderData.items[0]).preparation_time;

            foreach (var item in orderData.items)
            {
                if (maxPreparationTime < _itemsBuilder.GetItemDataByItemId(item).preparation_time)
                {
                    maxPreparationTime = _itemsBuilder.GetItemDataByItemId(item).preparation_time;
                }
            }

            orderData.max_wait =  Convert.ToInt32(maxPreparationTime * 1.3);

            return orderData;
        }
        
        
    }
}