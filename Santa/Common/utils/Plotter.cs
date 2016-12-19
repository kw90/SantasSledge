using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Common.utils
{
    public static class Plotter
    {
        public static void Plot(IEnumerable<Gift> gifts)
        {
            foreach (var gift in gifts)
            {
                Debug.WriteLine("{0},{1}", gift.Location.Latitude, gift.Location.Longitude);
            }
        }

        public static void PlotInfo(IEnumerable<Gift> gifts)
        {
            Console.WriteLine("number of gifts: {0}", gifts.Count());
            Console.WriteLine("total weight: {0}", gifts.Select(g => g.Weight).Sum());
        }
    }
}
