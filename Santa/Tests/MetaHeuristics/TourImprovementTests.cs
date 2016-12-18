using Common;
using MetaHeuristics;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests.MetaHeuristics
{
    public class TourImprovementTests
    {
        [Test]
        public void ReverseTours()
        {
            Tour tour1 = new Tour();
            tour1.AddGift(new Gift(1, 10.0, 10.0, 20.0));
            tour1.AddGift(new Gift(2, 40.0, 13.0, 26.0));

            Tour tour2 = new Tour();
            tour2.AddGift(new Gift(3, 60.0, 70.0, 50.0));
            tour2.AddGift(new Gift(4, 45.0, 113.0, 96.0));

            List<Tour> result = RouteImprovement.ReverseTours(tour1, tour2) as List<Tour>;
            Assert.AreEqual(2, result[0].Gifts[0].Id);
            Assert.AreEqual(1, result[0].Gifts[1].Id);
            Assert.AreEqual(4, result[1].Gifts[0].Id);
            Assert.AreEqual(3, result[1].Gifts[1].Id);
        }

        [Test]
        public void FindOptimalInsertionPosition()
        {
            Tour tour = new Tour();
            tour.AddGift(new Gift(1, 10.0, 10.0, 20.0));
            tour.AddGift(new Gift(2, 10.0, 10.0, 30.0));
            tour.AddGift(new Gift(3, 10.0, 10.0, 40.0));
            tour.AddGift(new Gift(4, 10.0, 10.0, 50.0));
            // new Gift should be placed here
            tour.AddGift(new Gift(5, 10.0, 10.0, 60.0));
            tour.AddGift(new Gift(6, 10.0, 10.0, 70.0));

            Assert.AreEqual(4, RouteImprovement.FindOptimalInsertionPosition(tour, new Gift(7, 10.0, 10.0, 55.0)));
        }
    }
}
