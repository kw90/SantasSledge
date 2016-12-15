using System;
using System.Collections.Generic;
using Common;

namespace MetaHeuristics
{
	public class SimulatedAnnealing: ISolver
	{
		private double temperature = 1000000;

		private double COOLING_RATE = 0.003;

		public SimulatedAnnealing()
		{
		}

		private double acceptanceProbability(double energy, double newEnergy, double temperature)
		{
			if (newEnergy < energy) return 1.0;

			return Math.Exp((energy - newEnergy) / temperature);
		}

		public List<Gift> solve(List<Gift> gifts)
		{
			//List<Tour> tours = 
			return null;
		}
	}
}
