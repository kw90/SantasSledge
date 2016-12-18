using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace MetaHeuristics
{
    class SimulatedAnnealingTwoOpt : SimulatedAnnealing
    {
        public override List<Gift> Solve(List<Tour> tours)
        {
            int numberOfTours = tours.Count;
            List<Tour> bestTours = tours;
            double totalCurrentEnergy = Common.Algos.WeightedReindeerWeariness.Calculate(bestTours);

            while (Temperature > 1)
            {
                var maxIndex = numberOfTours - 1;
                int randomNumber1 = Random.Next(0, maxIndex);
                int randomNumber2 = Random.Next(0, maxIndex);

                Tour first = tours[randomNumber1];
                Tour second = tours[randomNumber2];
                double firstWRW = Common.Algos.WeightedReindeerWeariness.Calculate(first);
                double secondWRW = Common.Algos.WeightedReindeerWeariness.Calculate(second);
                double currentEnergy = firstWRW + secondWRW;

                List<Tour> changedTours = RouteImprovement.Swap(tours[randomNumber1], tours[randomNumber2]) as List<Tour>;

                first = changedTours[0];
                second = changedTours[1];
                firstWRW = Common.Algos.WeightedReindeerWeariness.Calculate(first);
                secondWRW = Common.Algos.WeightedReindeerWeariness.Calculate(second);
                double neighbourEnergy = firstWRW + secondWRW;

                if (AcceptanceProbability(currentEnergy, neighbourEnergy) > Random.Next(0, 1))
                {
                    tours[randomNumber1] = changedTours[0];
                    tours[randomNumber2] = changedTours[1];
                }

                var currentNewEnergy = Common.Algos.WeightedReindeerWeariness.Calculate(tours);
                Console.WriteLine("{0}, {1}", totalCurrentEnergy, currentNewEnergy);
                if (totalCurrentEnergy > currentNewEnergy)
                {
                    bestTours = tours;
                    totalCurrentEnergy = currentNewEnergy;
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
