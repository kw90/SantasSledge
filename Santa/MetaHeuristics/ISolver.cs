using System;
using System.Collections.Generic;
using Common;

namespace MetaHeuristics
{
	public interface ISolver
	{
		List<Gift> Solve(List<Tour> tours);
	}
}
