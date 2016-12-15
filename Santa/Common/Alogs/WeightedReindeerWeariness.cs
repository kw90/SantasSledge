using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Alogs
{
    public class WeightedReindeerWeariness
    {
        private static Location InitialLocation = new Location(90.0, 0.0);

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

            Location currentLocation = InitialLocation;
            double sleighWeight = tour.GetStartWeightOfTour() + Parameter.BaseSleighWeight;
            foreach(Gift gift in tour.Gifts)
            {
                weightedReindeerWeariness += (sleighWeight * currentLocation.DistanceTo(gift.Location));
                currentLocation = gift.Location;
                sleighWeight -= gift.Weight;
            }
            weightedReindeerWeariness += (sleighWeight * currentLocation.DistanceTo(InitialLocation));

            return 0.0;
        }
    }
}
