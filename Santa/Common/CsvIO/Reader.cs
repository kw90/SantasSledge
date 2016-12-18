using System;
using System.Collections.Generic;
using System.IO;

namespace Common.CsvIO
{
    public class Reader
    {
        public List<Gift> GetGifts(string filePath)
        {
            var reader = new StreamReader(File.OpenRead(filePath));

            var gifts = new List<Gift>();

            var header = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                var lineList = new Gift(
                    int.Parse(values[0]),
                    double.Parse(values[3]),
                    double.Parse(values[1]),
                    double.Parse(values[2]));

                gifts.Add(lineList);
            }

            return gifts;
        }

        public Area ReadArea(string areaPath)
        {
            var files = Directory.GetFiles(areaPath);
            var tours = new List<Tour>();
            foreach (var file in files)
            {
                var gifts = this.GetGifts(file);
                var tour = new Tour { Gifts = gifts };
                tours.Add(tour);
            }

            return new Area(tours);
        }
    }
}
