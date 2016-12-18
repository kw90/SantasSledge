using Common.CsvIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaHeuristics.Services
{
    public class SimulatedAnnealingService
    {
        const string areaPath = @"C:\Users\linri\Desktop\Santa\FirstSolution\Tours\europa";

        private readonly Reader reader;
        private readonly SimulatedAnnealing simulatedAnnealing;

        public SimulatedAnnealingService()
        {
            this.simulatedAnnealing = new SimulatedAnnealing();
            this.reader = new Reader();
        }

        public void Run()
        {
            var area = this.reader.ReadArea(areaPath);

            //this.simulatedAnnealing.Solve(area);
        }
    }
}
