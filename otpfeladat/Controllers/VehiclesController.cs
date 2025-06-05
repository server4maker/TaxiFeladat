using Microsoft.AspNetCore.Mvc;
using otpfeladat.Models;
using otpfeladat.Interfaces;



namespace otpfeladat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleRepository _repository;

        public VehiclesController(IVehicleRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult AddVehicle([FromBody] Vehicle vehicle)
        {
            _repository.Add(vehicle);
            return CreatedAtAction(nameof(GetAllVehicles), null);
        }

        [HttpGet]
        public IActionResult GetAllVehicles()
        {
            var vehicles = _repository.GetAll();
            return Ok(vehicles);
        }
    }
}
