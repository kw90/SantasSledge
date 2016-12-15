using Common;
using Common.Algos;
using System.Collections.Generic;
using System.Linq;

namespace FirstStep.Algos
{
    public class NearestNeighbour
    {
        public IEnumerable<Gift> GetTour(IEnumerable<Gift> giftsInput, double maxWeight)
        {
            if (giftsInput.Any() == false)
            {
                return new List<Gift>();
            }

            var gifts = giftsInput.ToList();
            var maxGifts = gifts.Count();
            
            var northPole = new NorthPole();
            var firstGift = GetNearestGift(northPole.Location, gifts);
            if(firstGift.Weight >= maxWeight)
            {
                return new List<Gift>();
            }

            var tour = new List<Gift>();
            tour.Add(firstGift);
            var lastGift = firstGift;
            gifts.Remove(lastGift);
            var currentWeight = lastGift.Weight;
            for(int i = 0; i < maxGifts - 1; i++)
            {
                var nextGift = GetNearestGift(lastGift.Location, gifts);
                currentWeight += nextGift.Weight;
                if(currentWeight <= maxWeight)
                {
                    lastGift = nextGift;
                    gifts.Remove(lastGift);
                    tour.Add(lastGift);
                }
                else
                {
                    break;
                }
            }

            return tour;
        }

        private Gift GetNearestGift(Location location, IEnumerable<Gift> gifts)
        {
            return gifts
                .OrderBy(g => g.Location.DistanceTo(location))
                .First();
        }
    }
}
