using System;
using System.Collections.Generic;
using Common;
using FirstSolution.Algos;

namespace MetaHeuristics
{
	public class SimulatedAnnealing : ISolver
	{
		double _temperature;
		Random random = new Random();
		double _coolingRate;

		public SimulatedAnnealing(double temperature, double coolingRate)
		{
			this._temperature = temperature;
			this._coolingRate = coolingRate;
		}

		public SimulatedAnnealing()
		{
			_temperature = 1000000;
			_coolingRate = 0.003;
		}

		private double acceptanceProbability(double energy, double newEnergy, double temperature)
		{
			if (newEnergy < energy) return 1.0;

			return Math.Exp((energy - newEnergy) / temperature);
		}

		public List<Gift> Solve(List<Tour> tours)
		{

            int numberOfTours = tours.Count;

			List<Tour> bestTours = new List<Tour>();

			while (_temperature > 1)
			{
				int randomNumber1 = random.Next(0, numberOfTours);
				int randomNumber2 = random.Next(0, numberOfTours);

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

				if (acceptanceProbability(currentEnergy, neighbourEnergy, _temperature) >= random.Next(0, 1))
				{
					tours[randomNumber1] = changedTours[0];
					tours[randomNumber2] = changedTours[1];
				}

				if (currentEnergy < Common.Algos.WeightedReindeerWeariness.Calculate(bestTours))
				{
					bestTours = tours;
				}

				_temperature *= 1 - _coolingRate;
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
