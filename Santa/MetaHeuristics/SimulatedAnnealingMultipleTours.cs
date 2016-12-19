using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Algos;
using Common.CsvIO;

namespace MetaHeuristics
{
    class SimulatedAnnealingMultipleTours : SimulatedAnnealing
    {
        public override List<Tour> Solve(List<Tour> tours)
        {
            List<Tour> bestTours = tours;
            double currentEnergy = Common.Algos.WeightedReindeerWeariness.Calculate(bestTours);

            while (Temperature > 1)
            {
                currentEnergy = Common.Algos.WeightedReindeerWeariness.Calculate(bestTours);
                //TODO RouteImprovement with group of tours 
                //List<Tour> changedTours = RouteImprovement.Swap(tours) as List<Tour>;

                Random rnd = new Random();
                int sdjdkflskdlfj = rnd.Next(0, tours.Count - 1);
                Tour tour1 = tours[sdjdkflskdlfj];
                Tour tour2 = new Tour();
                double distance = Double.MaxValue;
                for (int i = 0; i < tours.Count; i++)
                {
                    if (i != sdjdkflskdlfj)
                    {
                        double newDistance = tour1.GetMiddlePointOfTour().DistanceTo(tours[i].GetMiddlePointOfTour());
                        if (newDistance < distance)
                        {
                            distance = newDistance;
                            tour2 = tours[i];
                        }
                    }
                }
                /*tours.Remove(tour1);
                tours.Remove(tour2);*/

                List<Tour> changedTours = new List<Tour>(tours);
                changedTours.Remove(tour1);
                changedTours.Remove(tour2);
                List<Tour> b = RouteImprovement.Swap(tour1, tour2) as List<Tour>;
                changedTours.AddRange(b);

                double neighbourEnergy = Common.Algos.WeightedReindeerWeariness.Calculate(changedTours);

                if (AcceptanceProbability(currentEnergy, neighbourEnergy) > Random.Next(0, 1))
                {
                    tours = changedTours;
                }

                var currentNewEnergy = Common.Algos.WeightedReindeerWeariness.Calculate(changedTours);
                Console.WriteLine("{0}, {1}", currentEnergy, currentNewEnergy);
                if (currentEnergy > currentNewEnergy)
                {
                    bestTours = changedTours;
                }

                Writer writer = new Writer();
                writer.WriteSolution(AreaPath, "", bestTours);

                Temperature *= 1 - CoolingRate;
            }

            return bestTours;
        }
    }
}
