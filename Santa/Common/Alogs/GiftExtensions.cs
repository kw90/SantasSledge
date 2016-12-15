using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Alogs
{
    public static class GiftExtensions
    {
        public static double DistanceTo(this Gift one, Gift two)
        {
            const double radius = 6371000;

            var phyOne = ToRadiant(one.Latitude);
            var phyTwo = ToRadiant(two.Latitude);

            var lambdaOne = ToRadiant(one.Longitude);
            var lambdaTwo = ToRadiant(two.Longitude);

            var underRoot =
                PowTwo(Math.Sin((phyTwo - phyOne)/2))
                + Math.Cos(phyOne)
                * Math.Cos(phyTwo)
                * PowTwo(Math.Sin((lambdaTwo - lambdaOne)/2));

            var arg = Math.Sqrt(underRoot);

            return 
                2 
                * radius
                * Math.Asin(arg);
        }

        private static double PowTwo(double input)
        {
            return Math.Pow(input, 2);
        }

        private static double ToRadiant(double degrees)
        {
            return degrees / 360 * Math.PI * 2;
        }
    }
}
