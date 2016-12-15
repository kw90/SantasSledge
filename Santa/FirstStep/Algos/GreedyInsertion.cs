using Common;
using Common.Alogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstStep.Algos
{
    public class GreedyInsertion
    {
        public string Id
        {
            get
            {
                return "GreedyInsertion";
            }
        }

        public IEnumerable<Gift> Solve(Gift nordpole, IEnumerable<Gift> gifts, double maxWeight)
        {
            var points = gifts
                .OrderBy(p => p.Id)
                .ToList();
            points.Reverse();
            points.Add(nordpole);
            points.Reverse();
            points.ToArray();

            //Array with the indices of the next nodes
            int[] nextIndices = new int[points.Count()];

            //Initial partial tour 0 -> 1 -> 0
            nextIndices[0] = 1;
            double currentWeight = 0;

            if(points.Count == 2)
            {
                if(maxWeight < points.Select(g => g.Weight).Sum())
                {
                    return new List<Gift>();
                }
            }

            //Find the best position to insert for each remaining point
            for (int i = 2; i < points.Count(); i++)
            {
                double lowestDistanceIncrease = Double.PositiveInfinity;
                int lowestDistanceIncreaseIdx = -1;

                for (int j = 0; j < i; j++)
                {
                    //Increased cost of tour if point i is inserted in place j
                    double distanceIncrease =
                        points[j].DistanceTo(points[i])
                        + points[i].DistanceTo(points[nextIndices[j]])
                        - points[j].DistanceTo(points[nextIndices[j]]);

                    if (distanceIncrease < lowestDistanceIncrease)
                    {
                        lowestDistanceIncrease = distanceIncrease;
                        lowestDistanceIncreaseIdx = j;
                    }
                }

                currentWeight += nextIndices[lowestDistanceIncreaseIdx];
                if(currentWeight > maxWeight)
                {
                    break;
                }

                nextIndices[i] = nextIndices[lowestDistanceIncreaseIdx];
                nextIndices[lowestDistanceIncreaseIdx] = i;
            }

            //Walk along next indices to build solution.
            List<Gift> solution = new List<Gift>();
            int index = 0;
            for (int i = 0; i < points.Count(); i++)
            {
                solution.Add(points[index]);
                index = nextIndices[index];
            }

            return solution;
        }
    }
}
