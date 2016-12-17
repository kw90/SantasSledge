using System;
using System.Collections.Generic;
using Common;

namespace MetaHeuristics
{
	public class TwoOpt
	{
		public static IEnumerable<Tour> Swap(Tour tour1, Tour tour2)
		{
			tour2.Gifts.Reverse();
			tour1.Gifts.Reverse();
			return new List<Tour> { tour1, tour2 };
		}
	}
}
