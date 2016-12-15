using Common;
using Common.Algos;
using NUnit.Framework;
using System;

namespace Tests.Common
{
    [TestFixture]
    public class LocationExtensionsTests
    {
        private const double Precision = 0.0000001;
        private const double radius = 6371000;
        private double circle = 2 * radius * Math.PI;
        
        [Test]
        public void DistanceTo_From90La0LoTo0La0Lo_ReturnQuaterRadius()
        {
            double ExpectedDistance = circle / 4;

            var firstLocation = new Location(90, 0);
            var secondLocation = new Location(0, 0);

            var distance = firstLocation.DistanceTo(secondLocation);

            Assert.AreEqual(ExpectedDistance, distance, Precision);
        }

        [Test]
        public void DistanceTo_From90La0LoToMinus90La0Lo_ReturnHalfRadius()
        {
            double ExpectedDistance = circle / 2;

            var firstLocation = new Location(90, 0);
            var secondLocation = new Location(-90, 0);

            var distance = firstLocation.DistanceTo(secondLocation);

            Assert.AreEqual(ExpectedDistance, distance, Precision);
        }

        [Test]
        public void DistanceTo_From0La0LoTo0La180Lo_ReturnHalfRadius()
        {
            double ExpectedDistance = circle / 2;

            var firstLocation = new Location(0, 0);
            var secondLocation = new Location(0, 180);

            var distance = firstLocation.DistanceTo(secondLocation);

            Assert.AreEqual(ExpectedDistance, distance, Precision);
        }

        [Test]
        public void DistanceTo_From0La0LoTo0LaMinus90Lo_ReturnHalfRadius()
        {
            double ExpectedDistance = circle / 4;

            var firstLocation = new Location(0, 0);
            var secondLocation = new Location(0, -90);

            var distance = firstLocation.DistanceTo(secondLocation);

            Assert.AreEqual(ExpectedDistance, distance, Precision);
        }
    }
}
