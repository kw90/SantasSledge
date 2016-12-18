using System.Collections.Generic;

namespace Common
{
    public class Area
    {
        public IEnumerable<Gift> Gifts { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<Tour> Tours { get; private set; }

        public Area(IEnumerable<Tour> tours)
        {
            this.Tours = tours;
        }

        public Area(string name, List<Gift> gifts)
        {
            this.Name = name;
            this.Gifts = gifts;
        }

        public void AddTour(IEnumerable<Tour> tours)
        {
            this.Tours = tours;
        }
    }
}
