using Common;
using FirstStep.Algos;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests.FirstStep
{
    [TestFixture]
    public class GreedyInsertionTest
    {
        [Test]
        public void Solve_OnlyOneGiftAndTooHeavy_ReturnEmptyList()
        {
            var nordpole = new Gift(0, 0, 0, 0);
            var maxWeight = 10;
            var gifts = new List<Gift>
            {
                new Gift(1, 11, 0, 0)
            };

            var greedyInsertion = new GreedyInsertion();
            var tour = greedyInsertion.Solve(nordpole, gifts, maxWeight);

            Assert.AreEqual(0, tour.Count());
         }

        [Test]
        public void Solve_OnlyOneGiftAndWeightIsOk_ReturnListWithNorthPoleAndGift()
        {
            var nordpole = new Gift(0, 0, 0, 0);
            var maxWeight = 10;
            var gifts = new List<Gift>
            {
                new Gift(1, 9, 0, 0)
            };

            var greedyInsertion = new GreedyInsertion();
            var tour = greedyInsertion.Solve(nordpole, gifts, maxWeight).ToList();

            Assert.AreEqual(tour.First(), nordpole);
            Assert.AreEqual(tour[1], gifts.First());
        }

        [Test]
        public void Solve_()
        {
            var nordpole = new Gift(0, 0, 0, 0);
            var maxWeight = 10;
            var gifts = new List<Gift>
            {
                new Gift(1, 2, 0, 0),
                new Gift(2, 3, 1, 0),
                new Gift(3, 10, 2, 0)
            };

            var greedyInsertion = new GreedyInsertion();
            var tour = greedyInsertion.Solve(nordpole, gifts, maxWeight).ToList();
            
            Assert.AreEqual(tour[1], gifts.First());
            Assert.AreEqual(tour[2], gifts[1]);
            Assert.AreEqual(3, tour.Count());
        }
    }
}
