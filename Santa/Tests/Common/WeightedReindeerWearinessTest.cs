using Common;
using Common.Alogs;
using NUnit.Framework;

namespace Tests.Common
{
    public class WeightedReindeerWearinessTest
    {
        [Test]
        public void TestCalculation()
        {
            Tour tour = new Tour();
            Gift gift1 = new Gift(1, 15, 10, 10);
            Gift gift2 = new Gift(2, 30, 90, 30);
            tour.AddGift(gift1);
            tour.AddGift(gift2);

            double calculated = WeightedReindeerWeariness.Calculate(tour);
            double expected = (gift1.Weight + gift2.Weight + Parameter.BaseSleighWeight) * Parameter.InitialLocation.DistanceTo(gift1.Location) +
                (gift2.Weight + Parameter.BaseSleighWeight) * gift1.DistanceTo(gift2) +
                (Parameter.BaseSleighWeight * gift2.Location.DistanceTo(Parameter.InitialLocation));

            Assert.AreEqual(expected, calculated);
        }
    }
}
