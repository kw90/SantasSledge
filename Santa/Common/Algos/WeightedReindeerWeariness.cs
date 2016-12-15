using System.Collections.Generic;

namespace Common.Algos
{
    public class WeightedReindeerWeariness
    {
        public static double Calculate(List<Tour> tours)
        {
            double weightedReindeerWeariness = 0.0;

            foreach(Tour tour in tours)
            {
                weightedReindeerWeariness += Calculate(tour);
            }

            return weightedReindeerWeariness;
        }

        public static double Calculate(Tour tour)
        {
            double weightedReindeerWeariness = 0.0;

            Location currentLocation = Parameter.InitialLocation;
            double sleighWeight = tour.GetStartWeightOfTour() + Parameter.BaseSleighWeight;
            foreach(Gift gift in tour.Gifts)
            {
                weightedReindeerWeariness += (sleighWeight * currentLocation.DistanceTo(gift.Location));
                currentLocation = gift.Location;
                sleighWeight -= gift.Weight;
            }
            weightedReindeerWeariness += (sleighWeight * currentLocation.DistanceTo(Parameter.InitialLocation));

            return weightedReindeerWeariness;
        }
    }
}
