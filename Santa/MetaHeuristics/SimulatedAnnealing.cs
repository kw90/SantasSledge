using System;
using System.Collections.Generic;
using Common;
using FirstSolution.Algos;

namespace MetaHeuristics
{
	public abstract class SimulatedAnnealing : ISolver
	{
        protected double Temperature;
	    protected readonly Random Random = new Random();
        public string AreaPath { get; set; }
		protected double CoolingRate;

		public SimulatedAnnealing(double temperature, double coolingRate)
		{
			Temperature = temperature;
			CoolingRate = coolingRate;
		}

		public SimulatedAnnealing()
		{
			Temperature = 10000;
			CoolingRate = 0.003;
		}

		protected double AcceptanceProbability(double energy, double newEnergy)
		{
			if (newEnergy < energy) return 1.0;
            return Math.Exp((energy - newEnergy) / Temperature);
        }

	    public abstract List<Tour> Solve(List<Tour> tours);
	}
}
