namespace otpfeladat.Models
{
    public class Dtos
    {
        public List<VehicleDto> Vehicles { get; set; } = new();
        public double Profit { get; set; }
    }

    public class VehicleDto
    {
        public int Id { get; set; }
        public int PassengerCapacity { get; set; }
        public int Range { get; set; }
        public FuelType Fuel { get; set; }
    }
}
