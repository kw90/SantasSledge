using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class NorthPole
    {
        public Location Location { get; private set; }

        public NorthPole()
        {
            this.Location = new Location(90, 0);
        }
    }
}
