using Common;
using FirstStep.Algos;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests.FirstStep
{
    [TestFixture]
    public class NearestNeighbourTests
    {
        [Test]
        public void GetTour_GiftListEmpty_ReturnEmptyList()
        {
            var nearestNeighbour = new NearestNeighbour();
            var tour = nearestNeighbour.GetTour(new List<Gift>(), 0);

            Assert.AreEqual(0, tour.Count());
        }

        [Test]
        public void GetTour_OnlyOneGiftAndTooHeavy_ReturnEmptyList()
        {
            var maxWeight = 10;
            var gifts = new List<Gift>
            {
                new Gift(1, 11, 0, 0)
            };

            var nearestNeighbour = new NearestNeighbour();
            var tour = nearestNeighbour.GetTour(gifts, maxWeight);

            Assert.AreEqual(0, tour.Count());
         }

        [Test]
        public void GetTour_OnlyOneGiftAndWeightIsOk_ReturnListWithGift()
        {
            var maxWeight = 10;
            var gifts = new List<Gift>
            {
                new Gift(1, 9, 0, 0)
            };

            var nearestNeighbour = new NearestNeighbour();
            var tour = nearestNeighbour.GetTour(gifts, maxWeight).ToList();

            Assert.AreEqual(tour.First(), gifts.First());
        }

        [Test]
        public void GetTour_FiveGiftsWithTotalWeightUnderMaxWeight_ReturnListInCorrectOrderWithContainingAllGifts()
        {
            var maxWeight = 1000;
            var gifts = new List<Gift>
            {
                new Gift(1, 20, 0, 0),
                new Gift(2, 20, 30, 0),
                new Gift(3, 20, 70, 0),
                new Gift(4, 20, 10, 0),
                new Gift(5, 20, 5, 0)
            };

            var nearestNeighbour = new NearestNeighbour();
            var tour = nearestNeighbour.GetTour(gifts, maxWeight).ToList();
            
            Assert.AreEqual(tour[0], gifts[2]);
            Assert.AreEqual(tour[1], gifts[1]);
            Assert.AreEqual(tour[2], gifts[3]);
            Assert.AreEqual(tour[3], gifts[4]);
            Assert.AreEqual(tour[4], gifts[0]);
        }

        [Test]
        public void GetTour_FiveGiftsWithTotalWeightOverMaxWeight_ReturnListInCorrectOrderWithContainingOnlySubsetOfGifts()
        {
            var maxWeight = 40;
            var gifts = new List<Gift>
            {
                new Gift(1, 20, 3, 0),
                new Gift(2, 20, 2, 0),
                new Gift(3, 20, 60, 0),
                new Gift(4, 20, 1, 0),
                new Gift(5, 20, 80, 0)
            };

            var nearestNeighbour = new NearestNeighbour();
            var tour = nearestNeighbour.GetTour(gifts, maxWeight).ToList();

            Assert.AreEqual(2, tour.Count());
            Assert.AreEqual(tour[0], gifts[4]);
            Assert.AreEqual(tour[1], gifts[2]);
        }
    }
}
