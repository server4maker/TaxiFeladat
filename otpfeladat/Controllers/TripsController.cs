using Microsoft.AspNetCore.Mvc;
using otpfeladat.Interfaces;
using otpfeladat.Models;
using System.Linq;

namespace OTPFeladat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepo;
        private readonly ITripSuggestionService _tripSuggestionService;

        public TripsController(IVehicleRepository vehicleRepo, ITripSuggestionService tripSuggestionService)
        {
            _vehicleRepo = vehicleRepo;
            _tripSuggestionService = tripSuggestionService;
        }

        [HttpGet("suggestions")]
        public IActionResult GetSuggestions(int passengerCount, int distance)
        {
            var vehicles = _vehicleRepo.GetAll().ToList();
            var suggestions = _tripSuggestionService.GetSuggestions(passengerCount, distance, vehicles);

            return Ok(suggestions);
        }
    }
}
