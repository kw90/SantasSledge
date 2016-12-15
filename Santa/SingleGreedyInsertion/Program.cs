﻿using Common.CsvIO;
using Common.utils;
using FirstStep.Algos;
using System;

namespace SingleGreedyInsertion
{
    class Program
    {
        static void Main(string[] args)
        {
            const string path = @"C:\Users\linri\Desktop\Santa\europa.csv";
            const double maxWeight = 1000;

            var reader = new Reader();
            var gifts = reader.GetGifts(path);

            var tour = new GreedyInsertion().Solve(gifts, maxWeight);

            Plotter.Plot(tour);
            Plotter.PlotInfo(tour);

            Console.ReadLine();
        }
    }
}
