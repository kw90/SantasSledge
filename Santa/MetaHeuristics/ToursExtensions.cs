using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaHeuristics
{
    public static class ToursExtensions
    {
        public static IEnumerable<Tour> Clone(this IEnumerable<Tour> toursToClone)
        {
            var tours = new List<Tour>();
            foreach(var tour in toursToClone)
            {
                tours.Add(new Tour { Gifts = tour.Gifts.ToList() });
            }
            return tours;
        }
    }
}
