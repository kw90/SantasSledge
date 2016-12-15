﻿using Common;
using Common.Algos;
using System.Collections.Generic;
using System.Linq;

namespace FirstStep.Algos
{
    public class GreedyInsertion
    {
        public IEnumerable<Gift> Solve(IEnumerable<Gift> gifts, double maxWeight)
        {
            if (gifts.Any() == false)
            {
                return new List<Gift>();
            }

            var northPole = new NorthPole();

            gifts = gifts
                .OrderBy(g => g.Id);
            var tour = new List<Gift>();

            double totalWeight = 0;
            double currentDistance = 0;
            int indexToAdd = 0;
            foreach (var gift in gifts)
            {
                if (tour.Any() == false)
                {
                    currentDistance += 2 * northPole.Location.DistanceTo(gift.Location);
                }
                else
                {
                    var indexWithLowestLenthIncrease = -1;
                    var previosTourLenght = tour.Count();
                    var increase = double.PositiveInfinity;
                    for (int i = 0; i <= previosTourLenght; i++)
                    {
                        var currentIncrease = CalculateLenghtIncrease(northPole, tour, i, gift);
                        if (currentIncrease < increase)
                        {
                            indexWithLowestLenthIncrease = i;
                            increase = currentIncrease;
                        }
                    }
                    indexToAdd = indexWithLowestLenthIncrease;
                }

                totalWeight += gift.Weight;
                if (totalWeight > maxWeight)
                {
                    break;
                }
                else
                {
                    if(indexToAdd > tour.Count() - 1)
                    {
                        tour.Add(gift);
                    }
                    else
                    {
                        var before = tour
                            .Take(indexToAdd)
                            .ToList();

                        tour.Reverse();
                        var after = tour
                            .Take(tour.Count() - indexToAdd)
                            .Reverse();


                        before.Add(gift);
                        before.AddRange(after);

                        tour = before;
                    }
                }
            }

            return tour;
        }

        private double CalculateLenghtIncrease(NorthPole northPole, List<Gift> tour, int insertionIndex, Gift giftToInsert)
        {
            if(insertionIndex == 0)
            {
                var firtsInTour = tour.First();
                return giftToInsert.Location.DistanceTo(northPole.Location)
                    + giftToInsert.Location.DistanceTo(firtsInTour.Location)
                    - firtsInTour.Location.DistanceTo(northPole.Location);
            }

            if(insertionIndex == tour.Count)
            {
                var lastInTour = tour[tour.Count - 1];
                return giftToInsert.Location.DistanceTo(northPole.Location)
                    + giftToInsert.Location.DistanceTo(lastInTour.Location)
                    - lastInTour.Location.DistanceTo(northPole.Location);
            }

            var currentGiftAtPosition = tour[insertionIndex];
            var giftBeforePosition = tour[insertionIndex - 1];
            return giftToInsert.Location.DistanceTo(currentGiftAtPosition.Location)
                + giftToInsert.Location.DistanceTo(giftBeforePosition.Location)
                - currentGiftAtPosition.Location.DistanceTo(giftBeforePosition.Location);
        }
    }
}
