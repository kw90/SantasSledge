using System;
using System.Collections.Generic;
using Common;
using Common.Algos;

namespace MetaHeuristics
{
	public class RouteImprovement
	{
		public static IEnumerable<Tour> ReverseTours(Tour tour1, Tour tour2)
		{
			tour2.Gifts.Reverse();
			tour1.Gifts.Reverse();
			return new List<Tour> { tour1, tour2 };
		}

        public static IEnumerable<Tour> Swap(Tour tour1, Tour tour2, int pos1, int pos2)
        {
            List<Tour> retVal = new List<Tour>();

            Gift giftForTour2 = tour1.RemoveGift(pos1);
            Gift giftForTour1 = tour2.RemoveGift(pos2);

            tour1.AddGiftAtPos(giftForTour1, FindOptimalInsertionPosition(tour1, giftForTour1));
            tour2.AddGiftAtPos(giftForTour2, FindOptimalInsertionPosition(tour2, giftForTour2));

            retVal.Add(tour1);
            retVal.Add(tour2);

            return retVal;
        }

        public static int FindOptimalInsertionPosition(Tour tour, Gift gift)
        {
            double minWeariness = double.MaxValue;
            int positionToInsert = 0;
            for (int i = 0; i < tour.Gifts.Count; i++)
            {
                tour.AddGiftAtPos(gift, i);
                double weariness = WeightedReindeerWeariness.Calculate(tour);
                if (weariness < minWeariness)
                {
                    minWeariness = weariness;
                    positionToInsert = i;
                }
                tour.RemoveGift(i);
            }

            return positionToInsert;
        }

        public static IEnumerable<Tour> Swap(List<Tour> tours)
        {
            Random rnd = new Random();

            int pos1 = rnd.Next(0, tours.Count);
            int pos2 = rnd.Next(0, tours.Count);

            Tour tour1 = tours[pos1];
            Tour tour2 = tours[pos2];

            tours.RemoveAt(pos1);
            tours.RemoveAt(pos2);

            tours.AddRange(Swap(tour1, tour2));

            return new List<Tour>() { tour1, tour2 };
        }

        public static IEnumerable<Tour> Swap(Tour tour1, Tour tour2)
        {
            Random rnd = new Random();
            int iteration = 0;

            while (iteration < tour1.Gifts.Count)
            {
                int pos1 = rnd.Next(0, tour1.Gifts.Count-1);
                int pos2 = rnd.Next(0, tour2.Gifts.Count-1);
                Gift gift1 = tour1.RemoveGift(pos1);
                Gift gift2 = tour2.RemoveGift(pos2);
                tour1.AddGiftAtPos(gift2, 0);
                tour2.AddGiftAtPos(gift1, 0);
                bool validChange = tour1.IsValid() && tour2.IsValid();
                tour1.RemoveGift(0);
                tour2.RemoveGift(0);
                tour1.AddGiftAtPos(gift1, pos1);
                tour2.AddGiftAtPos(gift2, pos2);
                if (validChange)
                {
                    return Swap(tour1, tour2, pos1, pos2);
                }
                iteration++;
            }
            return new List<Tour>() { tour1, tour2 };
        }

        public static IEnumerable<Tour> ReduceCrossings(List<Tour> tours)
        {
            throw new Exception("Not yet implemented!");
        }

        public static IEnumerable<Tour> ImproveFinalTour(IEnumerable<Tour> tours)
        {
            List<Tour> result = new List<Tour>();
            foreach(Tour tour in tours)
            {
                double oldWeariness = WeightedReindeerWeariness.Calculate(tour);
                tour.Gifts.Reverse();
                if(WeightedReindeerWeariness.Calculate(tour) < oldWeariness) {
                    result.Add(tour);
                } else
                {
                    tour.Gifts.Reverse();
                    result.Add(tour);
                }
            }
            return result;
        }
	}
}
