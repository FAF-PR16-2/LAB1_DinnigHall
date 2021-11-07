using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DinnerHall.Singleton;

namespace DinnerHall
{
    public class RandomOrdersGenerator : Singleton<RandomOrdersGenerator>
    {
        // int _lastOrderId;
        private int[] _tablesId;
        private int[] _waitersId;
        private int[] _priorities;

        private ItemsBuilder _itemsBuilder;

        private ItemData[] _possibleItemsData;

        public void Configure()
        {
            //_lastOrderId = 0;
            //_tablesId = Enumerable.Range(0, Configuration.GetInstance().TableCount).ToArray();
            _priorities = new []{1, 2, 3, 4, 5};
            _itemsBuilder = new ItemsBuilder();
            _possibleItemsData = _itemsBuilder.GetItems();
        }
        
        public OrderData OrderDataGenerator(int tableId)
        {
            var random = new Random();

            var orderData = new OrderData
            {
                order_id = new Guid(),
                table_id = tableId,
                waiter_id = 0,
                items = new int[random.Next(1, 5)],
                priority = _priorities[random.Next(_priorities.Length)],
            };

            for (var i = 0; i < orderData.items.Length; i++)
            {
                orderData.items[i] = _possibleItemsData[random.Next(_possibleItemsData.Length)].id;
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