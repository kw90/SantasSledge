using Common;
using Common.CsvIO;
using Common.utils;
using FirstSolution.Algos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SingleGreedyInsertion
{
    class Program
    {
        static void Main(string[] args)
        {
            const string path = @"C:\Users\linri\Desktop\Santa\europa.csv";
            const double maxWeight = 1000;

            var reader = new Reader();
            var gifts = reader.GetGifts(path).ToList();

            var tours = new List<Tour>();
            while (gifts.Count > 0)
            {
                var tour = new NearestNeighbour().GetTour(gifts, maxWeight);
                tours.Add(tour);
                gifts = gifts.Except(tour.Gifts).ToList();

                Console.WriteLine("Tours: {0}, Remaining gifts: {1}", tours.Count(), gifts.Count());
            }

            foreach(var tour in tours)
            {
                Plotter.Plot(tour.Gifts);
                Plotter.PlotInfo(tour.Gifts);
            }
            
            Console.ReadLine();
        }
    }
}
