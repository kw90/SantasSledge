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
