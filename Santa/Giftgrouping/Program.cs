using Common;
using Common.CsvIO;
using Common.utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Giftgrouping
{
    class Program
    {
        private static double longitudeAmericaEurope = -22;
        private static double northSouthAmarica = 13;
        private static double europaAfrica = 37.5;
        private static double longitueEuropeAsia = 51;
        private static double asiaAustralia = 8;
        private static double southPolar = -60;

        private static string workSpace = @"C:\Temp\";

        static void Main(string[] args)
        {
            var gifts = new Reader().GetGifts(workSpace + "gifts.csv");
            var totalGiftCount = gifts.Count();

            var groenland = gifts
                .Where(g => g.Location.Latitude > 60)
                .Where(g => g.Location.Longitude > -75)
                .Where(g => g.Location.Longitude < -0)
                .ToList();

            gifts = gifts.Except(groenland).ToList(); 

            Plot(groenland);
            PlotInfos(groenland);


            var southUnderPolar = South(gifts);
            var giftsForNorthAmerica = GetGiftsForNorthAmerica(gifts);

            var giftsForHaway = GetGiftsForHaway(giftsForNorthAmerica);
            giftsForNorthAmerica = giftsForNorthAmerica.Except(giftsForHaway).ToList();

            var weight = giftsForHaway
                .Select(g => g.Weight)
                .Sum();

            var forSouthAmerica = GetGiftsForSouthAmerica(gifts);
            var giftsForEurope = Europa(gifts);
            var africa = Aftrica(gifts);
            var giftsForAsia = Asia(gifts);

            var australia = Australia(gifts);

            var newSealand = australia
                .Where(g => g.Location.Longitude > 160)
                .Where(g => g.Location.Latitude < -35)
                .ToList();

            australia = australia.Except(newSealand).ToList();

            

            Console.WriteLine(totalGiftCount);
            Console.WriteLine(
                giftsForNorthAmerica.Count
                + groenland.Count
                + giftsForHaway.Count
                + forSouthAmerica.Count
                + giftsForEurope.Count
                + africa.Count
                + giftsForAsia.Count
                + australia.Count
                + newSealand.Count
                + southUnderPolar.Count);

            var writer = new Writer();

            writer.Write(workSpace, "northAmerica", giftsForNorthAmerica);
            writer.Write(workSpace, "groenland", groenland);
            writer.Write(workSpace, "haway", giftsForHaway);
            writer.Write(workSpace, "southAmerica", forSouthAmerica);
            writer.Write(workSpace, "europa", giftsForEurope);
            writer.Write(workSpace, "africa", africa);
            writer.Write(workSpace, "asia", giftsForAsia);
            writer.Write(workSpace, "austrialia", australia);
            writer.Write(workSpace, "newSealand", newSealand);
            writer.Write(workSpace, "southPole", southUnderPolar);

            Console.ReadLine();
        }

        private static void PlotInfos(List<Gift> gifts)
        {
            Plotter.PlotInfo(gifts);
        }

        private static List<Gift> GetGiftsForHaway(List<Gift> gifs)
        {
            return gifs
                .Where(g => g.Location.Latitude > 0)
                .Where(g => g.Location.Latitude < 24)
                .Where(g => g.Location.Longitude < -150)
                .ToList();
        }

        private static void Plot(List<Gift> gifts)
        {
            var subset = gifts.Take(100);
            Plotter.Plot(subset);
        }

        private static List<Gift> South(List<Gift> gifts)
        {
            return gifts
                .Where(g => g.Location.Latitude < -60)
                .ToList();
        }

        private static List<Gift> Asia(List<Gift> gifts)
        {
            return AsiaAndAustralia(gifts)
                .Where(g => g.Location.Latitude > asiaAustralia)
                .ToList();
        }

        private static List<Gift> Australia(List<Gift> gifts)
        {
            return AsiaAndAustralia(gifts)
                .Where(g => g.Location.Latitude < asiaAustralia)
                .Where(g => g.Location.Latitude > southPolar)
                .ToList();
        }

        private static IEnumerable<Gift> AsiaAndAustralia(List<Gift> gifts)
        {
            return gifts
                .Where(g => g.Location.Longitude > longitueEuropeAsia);
        }

        private static List<Gift> Aftrica(List<Gift> gifts)
        {
            return EuropaAfrica(gifts)
                .Where(g => g.Location.Latitude < europaAfrica)
                .Where(g => g.Location.Latitude > southPolar)
                .ToList();
        }

        private static List<Gift> Europa(List<Gift> gifts)
        {
            return EuropaAfrica(gifts)
                .Where(g => g.Location.Latitude > europaAfrica)
                .ToList();
        }

        private static IEnumerable<Gift> EuropaAfrica(List<Gift> gifts)
        {
            return gifts
                .Where(g => g.Location.Longitude > longitudeAmericaEurope)
                .Where(g => g.Location.Longitude < longitueEuropeAsia);
        }

        private static List<Gift> GetGiftsForNorthAmerica(List<Gift> gifts)
        {
            return GetAmerica(gifts)
                .Where(g => g.Location.Latitude > northSouthAmarica)
                .ToList();
        }

        private static List<Gift> GetGiftsForSouthAmerica(List<Gift> gifts)
        {
            return GetAmerica(gifts)
                .Where(g => g.Location.Latitude < northSouthAmarica)
                .Where(g => g.Location.Latitude > -60)
                .ToList();
        }

        private static IEnumerable<Gift> GetAmerica(List<Gift> gifts)
        {
            return gifts
                .Where(g => g.Location.Longitude < longitudeAmericaEurope);
        }
    }
}
