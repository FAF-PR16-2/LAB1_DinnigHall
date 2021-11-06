using System;
using System.Collections.Generic;
using System.Linq;
using DinnerHall.Singleton;

namespace DinnerHall
{
    public class RatingManager : Singleton<RatingManager>
    {
        private const int maxRatingsInQueue = 100;
        
        private readonly object avgRatingLock = new object();
        private double avgRating;

        private Queue<int> ratingsQueue;

        public RatingManager()
        {
            ratingsQueue = new Queue<int>();
        }
        

        public void UpdateAvgRating(int newRating)
        {
            lock(avgRatingLock)
            {

                if (ratingsQueue.Count >= maxRatingsInQueue)
                    ratingsQueue.Dequeue();
                
                ratingsQueue.Enqueue(newRating);

                double newAvgRating = ratingsQueue.Average();
                
                avgRating = newAvgRating;

                ShowAvgRating();
            }
        }

        private void ShowAvgRating()
        {
            Console.WriteLine("Avg Rating: "  + avgRating);
        }
        
    }
}