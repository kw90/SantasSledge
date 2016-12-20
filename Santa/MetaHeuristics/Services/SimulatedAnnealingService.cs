using System.IO;
using Common.CsvIO;
using System.Linq;

namespace MetaHeuristics.Services
{
    public class SimulatedAnnealingService
    {
        const string areaPath = @"C:\temp\tours\";

        private readonly Reader reader;
        private readonly SimulatedAnnealingMultipleTours simulatedAnnealing;

        private string area;

        public SimulatedAnnealingService(string area)
        {
            this.simulatedAnnealing = new SimulatedAnnealingMultipleTours();
            this.reader = new Reader();
            this.area = area;
        }

        public void Run()
        {
            var area = this.reader.ReadArea(Path.Combine(areaPath, this.area));
            simulatedAnnealing.AreaPath = Path.Combine(areaPath, this.area);
            this.simulatedAnnealing.Solve(area.Tours.ToList());
        }
    }
}
