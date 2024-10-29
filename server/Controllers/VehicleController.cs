using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Interfaces;
using server.Mappers;

namespace server.Controllers
{
    [Route("api/vehicle")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleController(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var vehicleModel = await _vehicleRepository.GetAllVehicles();


            var vehicleDto = vehicleModel.Select(v => v.ToVehicleDto());

            return Ok(vehicleDto);
        }
    }
}
