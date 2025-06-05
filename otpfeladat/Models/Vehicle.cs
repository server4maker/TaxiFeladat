    namespace otpfeladat.Models
    {
        public enum FuelType
        {
            Gasoline,
            MildHybrid,
            PureElectric
        }

        public class Vehicle
        {
            public int Id { get; set; }
            public int PassengerCapacity { get; set; }
            public int Range { get; set; }
            public FuelType Fuel { get; set; }
        }
    }
