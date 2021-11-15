using System;
using DinnerHall.Singleton;

namespace DinnerHall
{
    public class Configuration // : Singleton<Configuration>
    {
        public const int TableCount = 10;
        public const int WaitersCount = 4;

        public const int TimeUnit = 100; // in milliseconds
        public const int TimeMinWaitOrder = TimeUnit * 20;
        public const int TimeMaxWaitOrder = TimeUnit * 40;
    }
}