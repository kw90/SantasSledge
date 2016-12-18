using System.Collections.Generic;
using Common;
using System.Linq;
using System;

namespace FirstSolution
{
    public class Validator
    {
        public void Validate(IEnumerable<Gift> totalGifts, IEnumerable<Area> areas)
        {
            var totalGiftCount = totalGifts.Count();
            int areaGiftCount = 0;
            foreach (var area in areas)
            {
                areaGiftCount += area.Gifts.Count();
            }

            if(totalGiftCount != areaGiftCount)
            {
                throw new Exception(string.Format("Total gift count {0}, gift count in areas {1}", totalGiftCount, areaGiftCount));
            }

            //var collectedGifts = areas
            //    .SelectMany(l => l)
            //    .Select(g => g.Id);

            //foreach(var gift in totalGifts)
            //{
            //    if (collectedGifts.Contains(gift.Id) == false)
            //    {
            //        throw new Exception(string.Format("Gift with Id {0} not present in one are", gift.Id));
            //    }
            //}
        }
    }
}
