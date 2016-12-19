using System;
using System.Collections.Generic;
using Common;

namespace MetaHeuristics
{
	public interface ISolver
	{
		List<Tour> Solve(List<Tour> tours);
	}
}
