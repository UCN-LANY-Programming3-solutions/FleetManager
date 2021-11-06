namespace FleetManager.Desktop.Model
{
    public class CarModel
    {
        public int? Id { get; set; }
        public string Brand { get; set; }
        public int Mileage { get; set; }
        public LocationModel Location { get; set; }

        public override string ToString()
        {
            return $"{Brand}";
        }
    }
}
