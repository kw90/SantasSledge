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
            var phyOne = one.Latitude / 360 * Math.PI * 2;
            //var phyTwo 

            return 0;
        }

        private static double ToRadiant(double degrees)
        {
            return degrees / 360 * Math.PI * 2;
        }
    }
}
