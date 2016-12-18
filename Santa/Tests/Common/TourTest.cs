using Common;
using NUnit.Framework;

namespace Tests.Common
{
    [TestFixture]
    public class TourTest
    {
        private Tour testee;

        [SetUp]
        public void Init()
        {
            testee = new Tour();
            testee.AddGift(new Gift(1, 500, 0, 0));
            testee.AddGift(new Gift(2, 500, 4, 2));
        }

        [Test]
        public void TestAddGiftAtPos()
        {
            int insertPos = 1;
            Gift gift = new Gift(99, 15.0, 90.0, 30.0);
            testee.AddGiftAtPos(gift, insertPos);
            Assert.AreEqual(gift, testee.Gifts[insertPos]);
        }

        [Test]
        public void TestRemoveGift()
        {
            int oldCount = testee.Gifts.Count;
            int positionToRemove = 1;
            Gift removed = testee.RemoveGift(positionToRemove);

            Assert.AreEqual(oldCount - 1, testee.Gifts.Count);
            foreach(Gift gift in testee.Gifts)
            {
                Assert.AreNotEqual(removed, gift);
            }
        }

        [Test]
        public void TestValidTourWeight()
        {
            Assert.IsTrue(testee.IsValid());
        }

        [Test]
        public void TestInvalidTourWeight()
        {
            testee.AddGift(new Gift(3, 0.001, 5, 5));
            Assert.IsFalse(testee.IsValid());
        }

        [Test]
        public void TestAddGift()
        {
            int oldCount = testee.Gifts.Count;
            testee.AddGift(new Gift(3, 0.001, 5, 5));
            Assert.AreEqual(oldCount + 1, testee.Gifts.Count);
        }

        [Test]
        public void TestGetStartWeightOftour()
        {
            Assert.AreEqual(1000.0, testee.GetStartWeightOfTour());
            testee.AddGift(new Gift(3, 0.001, 5, 5));
            Assert.AreEqual(1000.001, testee.GetStartWeightOfTour());
        }
    }
}
