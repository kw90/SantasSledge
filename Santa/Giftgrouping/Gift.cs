namespace Giftgrouping
{
    public class Gift
    {
        public int Id { get; private set; }
        public double Weight { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public Gift(
            int id,
            double weight,
            double latitude,
            double longitude)
        {
            this.Id = id;
            this.Weight = weight;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}
