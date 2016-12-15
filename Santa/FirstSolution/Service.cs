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
        private readonly Writer writer;

        public Service()
        {
            this.validator = new Validator();
            this.reader = new Reader();
            this.writer = new Writer();
        }

        public void Run()
        {
            var files = GetAllFiles(workSpace);
            var areas = GetAreas(files);
            var total = this.reader.GetGifts(workSpace + @"\Total\gifts.csv");

            this.validator.Validate(total, areas);
            Parallel.ForEach(areas, area =>
            {
                var gifts = area.Gifts;
                var tours = new List<Tour>();
                while (gifts.Count() > 0)
                {
                    var tour = new NearestNeighbour().GetTour(gifts, maxWeight);
                    tours.Add(tour);
                    gifts = gifts.Except(tour.Gifts).ToList();

                    Console.WriteLine("Tours: {0}, Remaining gifts: {1}", tours.Count(), gifts.Count());
                }

                area.AddTour(tours);
                Console.WriteLine("Finished for area: ");

                //foreach (var tour in tours)
                //{
                //    //Plotter.Plot(tour.Gifts);
                //    Plotter.PlotInfo(tour.Gifts);
                //}
            });

            Write(areas);

            //Console.WriteLine("Total tour count: {0}", areas.SelectMany(t => t).Count());
            Console.ReadLine();
        }

        private void Write(IEnumerable<Area> areas)
        {
            foreach (var area in areas)
            {
                var dirPath = workSpace + @"\Tours\" + area.Name + @"\"; 
                Directory.CreateDirectory(dirPath);
                var tourId = 0;
                foreach (var tour in area.Tours)
                {
                    tourId++; ;
                    this.writer.Write(dirPath, tourId.ToString(), tour.Gifts);
                }
            }
        }

        private IEnumerable<Area> GetAreas(IEnumerable<string> files)
        {
            var areas = new List<Area>();
            foreach(var file in files)
            {
                var name = Path.GetFileNameWithoutExtension(file);
                var gifts = this.reader.GetGifts(file);
                var area = new Area(name, gifts);
                areas.Add(area);
            }
            return areas;
        }

        private IEnumerable<string> GetAllFiles(string workSpace)
        {
            return Directory.GetFiles(workSpace);
        }
    }
}
