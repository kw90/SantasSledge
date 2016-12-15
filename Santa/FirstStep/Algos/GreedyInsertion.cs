using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IEnumerable<Gift> Solve(IEnumerable<Gift> pointsInput)
        {
            var points = pointsInput
                .OrderBy(p => p.Id)
                .ToArray();

            //Array with the indices of the next nodes
            int[] nextIndices = new int[points.Count()];

            //Initial partial tour 0 -> 1 -> 0
            nextIndices[0] = 1;

            //Find the best position to insert for each remaining point
            for (int i = 2; i < points.Count(); i++)
            {
                double lowestDistanceIncrease = Double.PositiveInfinity;
                int lowestDistanceIncreaseIdx = -1;

                for (int j = 0; j < i; j++)
                {
                    //Increased cost of tour if point i is inserted in place j
                    double distanceIncrease =
                        points[j].CalculateDistanceTo(points[i])
                        + points[i].CalculateDistanceTo(points[nextIndices[j]])
                        - points[j].CalculateDistanceTo(points[nextIndices[j]]);

                    if (distanceIncrease < lowestDistanceIncrease)
                    {
                        lowestDistanceIncrease = distanceIncrease;
                        lowestDistanceIncreaseIdx = j;
                    }
                }

                nextIndices[i] = nextIndices[lowestDistanceIncreaseIdx];
                nextIndices[lowestDistanceIncreaseIdx] = i;
            }

            //Walk along next indices to build solution.
            List<Point> solution = new List<Point>();
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
