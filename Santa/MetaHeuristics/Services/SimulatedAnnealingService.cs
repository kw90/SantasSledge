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
        const string areaPath = @"C:\Users\tcfux\Source\Repos\SantasSledge\FirstSolutions\europa";

        private readonly Reader reader;
        private readonly SimulatedAnnealingMultipleTours simulatedAnnealing;

        public SimulatedAnnealingService()
        {
            this.simulatedAnnealing = new SimulatedAnnealingMultipleTours();
            this.reader = new Reader();
        }

        public void Run()
        {
            var area = this.reader.ReadArea(areaPath);
            simulatedAnnealing.AreaPath = areaPath;
            this.simulatedAnnealing.Solve(area.Tours.ToList());
        }
    }
}
