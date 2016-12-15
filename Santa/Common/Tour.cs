using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Tour
    {
        private List<Gift> Gifts;

        public List<Gift> GetTour()
        {
            return Gifts;
        }

        public void AddGift(Gift gift)
        {
            Gifts.Add(gift);
        }
    }
}
