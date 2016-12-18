using System;

namespace Common
{
    public class Gift
    {
        public int Id { get; private set; }
        public double Weight { get; private set; }
        public Location Location { get; private set; }

        public Gift(
            int id,
            double weight,
            double latitude,
            double longitude)
        {
            this.Id = id;
            this.Weight = weight;
            this.Location = new Location(latitude, longitude);
        }
    }
}
