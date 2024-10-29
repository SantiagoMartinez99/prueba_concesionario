using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Dtos.Motorcycle;
using server.Interfaces;
using server.Mappers;

namespace server.Controllers
{
    [Route("api/motorcycle")]
    [ApiController]

    public class MotorcycleController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMotorcycleRepository _motorcycleRepository;

        public MotorcycleController(ApplicationDBContext context, IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMotorcycles()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var motorcycleModel = await _motorcycleRepository.GetAllMotorcycles();
            var carDto = motorcycleModel.Select(v => v.ToMotorcycleDto());
            return Ok(motorcycleModel);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMotorcycleById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var motorcycleModel = await _motorcycleRepository.GetMotorcycleById(id);
            if (motorcycleModel == null)
            {
                return NotFound();
            }
            return Ok(motorcycleModel.ToMotorcycleDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateMotorcycle([FromBody] CreateMotorcycleRequestDto motorcycleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var carCount = await _motorcycleRepository.GetTotalMotorcycleCountAsync();
            if (carCount >= 10)
            {
                ModelState.AddModelError("CarLimit", "No se pueden tener más de 10 carros en total.");
                return BadRequest(ModelState);
            }

            var motorcycleModel = motorcycleDto.ToMotorcycleFromCreateDto();
            await _motorcycleRepository.CreateMotorcycle(motorcycleModel);
            return CreatedAtAction(nameof(GetMotorcycleById), new { id = motorcycleModel.Id }, motorcycleModel.ToMotorcycleDto());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateMotorcycle([FromRoute] int id, [FromBody] UpdateMotorcycleRequestDto updateMotorcycleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var motorcycleModel = await _motorcycleRepository.UpdateMotorcycle(id, updateMotorcycleDto);

            if (motorcycleModel == null)
            {
                return NotFound();
            }

            return Ok(motorcycleModel.ToMotorcycleDto());

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMotorcycle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var motorcycleModel = await _motorcycleRepository.DeleteMotorcycle(id);
            if (motorcycleModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}