using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class Tour
    {
        public List<Gift> Gifts { get; private set; }

        public Tour()
        {
            Gifts = new List<Gift>();
        }

        public void AddGift(Gift gift)
        {
            Gifts.Add(gift);
        }

        public bool IsValid()
        {
            return Gifts.Aggregate(0.0, (current, gift) => current + gift.Weight) <= Parameter.MaxWeight;
        }
    }
}
