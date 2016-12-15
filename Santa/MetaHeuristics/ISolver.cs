using System;
using System.Collections.Generic;
using Common;

namespace MetaHeuristics
{
	public interface ISolver
	{
		List<Gift> solve(List<Gift> gifts);
	}
}
