using MetaHeuristics.Services;

namespace SimulatedAnnealingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new SimulatedAnnealingService();
            service.Run();
        }
    }
}
