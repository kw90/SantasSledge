using Common;
using Common.CsvIO;
using Common.utils;
using FirstSolution.Algos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FirstSolution
{
    public class Service
    {
        const double maxWeight = 1000;
        const string workSpace = @"C:\Users\linri\Desktop\Santa\FirstSolution";

        private readonly Validator validator;
        private readonly Reader reader;

        public Service()
        {
            this.validator = new Validator();
            this.reader = new Reader();
        }

        public void Run()
        {
            var files = GetAllFiles(workSpace);
            var areas = GetAreas(files);
            var total = this.reader.GetGifts(workSpace + @"\Total\gifts.csv");

            this.validator.Validate(total, areas);
            var areaTours = new List<IEnumerable<Tour>>();
            Parallel.ForEach(areas, area =>
            {
                var gifts = area;
                var tours = new List<Tour>();
                while (gifts.Count() > 0)
                {
                    var tour = new NearestNeighbour().GetTour(gifts, maxWeight);
                    tours.Add(tour);
                    gifts = gifts.Except(tour.Gifts).ToList();

                    Console.WriteLine("Tours: {0}, Remaining gifts: {1}", tours.Count(), gifts.Count());
                }

                areaTours.Add(tours);
                Console.WriteLine("Finished for area: ");

                //foreach (var tour in tours)
                //{
                //    //Plotter.Plot(tour.Gifts);
                //    Plotter.PlotInfo(tour.Gifts);
                //}
            });

            Console.WriteLine("Total tour count: {0}", areaTours.SelectMany(t => t).Count());
            Console.ReadLine();
        }

        private IEnumerable<IEnumerable<Gift>> GetAreas(IEnumerable<string> files)
        {
            var areas = new List<IEnumerable<Gift>>();
            foreach(var file in files)
            {
                areas.Add(this.reader.GetGifts(file));
            }
            return areas;
        }

        private IEnumerable<string> GetAllFiles(string workSpace)
        {
            return Directory.GetFiles(workSpace);
        }
    }
}
