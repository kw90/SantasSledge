using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace MetaHeuristics
{
    class SimulatedAnnealingMultipleTours : SimulatedAnnealing
    {
        public override List<Gift> Solve(List<Tour> tours)
        {
            List<Tour> bestTours = tours;
            double currentEnergy = Common.Algos.WeightedReindeerWeariness.Calculate(bestTours);

            while (Temperature > 1)
            {
                //TODO RouteImprovement with group of tours 
                List<Tour> changedTours = RouteImprovement.Swap(tours) as List<Tour>;
                
                double neighbourEnergy = Common.Algos.WeightedReindeerWeariness.Calculate(changedTours);

                if (AcceptanceProbability(currentEnergy, neighbourEnergy) > Random.Next(0, 1))
                {
                    tours = changedTours;
                }

                var currentNewEnergy = Common.Algos.WeightedReindeerWeariness.Calculate(tours);
                Console.WriteLine("{0}, {1}", currentEnergy, currentNewEnergy);
                if (currentEnergy > currentNewEnergy)
                {
                    bestTours = tours;
                }

                Temperature *= 1 - CoolingRate;
            }

            List<Gift> bestGiftOrder = new List<Gift>();

            foreach (Tour tour in bestTours)
            {
                bestGiftOrder.AddRange(tour.Gifts);
            }

            return bestGiftOrder;
        }
    }
}
