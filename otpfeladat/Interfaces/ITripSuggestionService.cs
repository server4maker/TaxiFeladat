using otpfeladat.Models;

namespace otpfeladat.Interfaces
{
    public interface ITripSuggestionService
    {
        IEnumerable<Dtos> GetSuggestions(int passengerCount, int distance, List<Vehicle> vehicles);
    }
}
