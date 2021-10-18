using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DinnerHall
{
    public class Table
    {
        public enum TableStatus
        {
            Free,
            MakingOrder,
            WaitingForOrder
        }
        
        public int  Id => _id;
        public TableStatus Status;
        public OrderData CurrentOrderData;
        
        private int _id;
        private Thread _thread;
        private Mutex _mutex;
        private EventWaitHandle _waitHandle;


        public Table(int id)
        {
            _id = id;
            _thread = new Thread(Update);
            _mutex = new Mutex();
            _waitHandle = new ManualResetEvent(false);
        }

        public void Start()
        {
            _thread.Start();
        }

        private void Update()
        {
            while (true)
            {
                ChangeStatusToFree(); // First iteration all tables are free
                ChangeStatusToMakingOrder();
                 
                _waitHandle.WaitOne();
                
            }
        }

        private void ChangeStatusToFree()
        {
            Status = TableStatus.Free;
            Thread.Sleep(Configuration.TimeUnit * 100);
        }
        
        private void ChangeStatusToMakingOrder()
        {
            Status = TableStatus.MakingOrder;
            _waitHandle.Reset();
        }

        public bool MakeOrder()
        {
            _mutex.WaitOne();
            if (Status != TableStatus.MakingOrder)
                return false;

            
            CurrentOrderData = RandomOrdersGenerator.GetInstance().OrderDataGenerator(Id);

            Status = TableStatus.WaitingForOrder;
            Console.WriteLine("-> Thread:" + Thread.CurrentThread.ManagedThreadId + " Table ID:" + Id + " Order ID:" + CurrentOrderData.order_id);// +  " ----- \n" + orderData + "\n ----- \n" );
            _mutex.ReleaseMutex();

            return true;
        }

        public void ReceiveOrder()
        {
            
            _waitHandle.Set();
            Console.WriteLine("<- Thread:" + Thread.CurrentThread.ManagedThreadId + " Table ID:" + Id + " Order ID:" + CurrentOrderData.order_id);// +  " ----- \n" + orderData + "\n ----- \n" );
        }
        
    }
}