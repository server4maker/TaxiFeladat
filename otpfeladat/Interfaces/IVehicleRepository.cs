using otpfeladat.Models;

namespace otpfeladat.Interfaces
{
    public interface IVehicleRepository
    {
        void Add(Vehicle vehicle);
        IEnumerable<Vehicle> GetAll();

    }
}
