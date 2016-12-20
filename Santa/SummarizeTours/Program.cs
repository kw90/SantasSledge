using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Algos;
using Common.CsvIO;
using MetaHeuristics;

namespace SummarizeTours
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\temp\tourout\";
            StringBuilder builder = new StringBuilder();
            List<Tour> tours = new List<Tour>();
            foreach (string file in Directory.GetFiles(path).Where((x) => x.EndsWith(".csv")))
            {
                string[] lines = File.ReadAllLines(file);
                int tourCountInFile = -1;
                Tour tour = null;
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] giftTrip = lines[i].Split(',');
                    if (int.Parse(giftTrip[1]) != tourCountInFile)
                    {
                        tourCountInFile = int.Parse(giftTrip[1]);
                        if (tour != null) tours.Add(tour);
                        tour = new Tour();
                    }
                    Gift g = new Gift(int.Parse(giftTrip[0]), 0.0,0.0,0.0);
                    tour.AddGift(g);
                }
                tours.Add(tour);
            }
            tours = RouteImprovement.ImproveFinalTour(tours).ToList();
            Console.WriteLine("Weight: {0}", WeightedReindeerWeariness.Calculate(tours));
            Writer writer = new Writer();
            writer.WriteSolution(path, "submission6", tours);
            Console.ReadLine();
        }
    }
}
