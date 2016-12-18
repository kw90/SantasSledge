using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class Tour
    {
        public List<Gift> Gifts { get; set; }

        public Tour()
        {
            Gifts = new List<Gift>();
        }

        public void AddGift(Gift gift)
        {
            Gifts.Add(gift);
        }

        public void AddGiftAtPos(Gift gift, int pos)
        {
            Gifts.Insert(pos, gift);
        }

        public Gift RemoveGift(int position)
        {
            Gift deleted = Gifts[position];
            Gifts.RemoveAt(position);
            return deleted;
        }

        public double GetStartWeightOfTour()
        {
            return Gifts.Aggregate(0.0, (current, gift) => current + gift.Weight);
        }

        public bool IsValid()
        {
            return GetStartWeightOfTour() <= Parameter.MaxWeight;
        }
    }
}
