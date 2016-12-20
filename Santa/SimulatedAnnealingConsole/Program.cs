using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MetaHeuristics.Services;

namespace SimulatedAnnealingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\temp\tours\";
            var directories = Directory.GetDirectories(path);
            foreach (var directory in directories)
            {
                new Thread(new ThreadStart(() =>
                {
                    var service = new SimulatedAnnealingService(directory);
                    service.Run();
                })).Start();

            }

            Console.ReadLine();
        }
    }
}
