using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Dtos.Car;
using server.Interfaces;
using server.Mappers;
using server.Models;

namespace server.Controllers
{
    [Route("api/car")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ICarRepository _carRepository;
        public CarController(ApplicationDBContext context, ICarRepository carRepository)
        {
            _carRepository = carRepository;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var carModel = await _carRepository.GetAllCars();
            var carDto = carModel.Select(v => v.ToCarDto());

            return Ok(carModel);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCarById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var carModel = await _carRepository.GetCarById(id);

            if (carModel == null)
            {
                return NotFound();
            }

            return Ok(carModel.ToCarDto());
        }
        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] CreateCarRequestDto carDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var carCount = await _carRepository.GetTotalCarCountAsync();
            if (carCount >= 10)
            {
                ModelState.AddModelError("CarLimit", "No se pueden tener más de 10 carros en total.");
                return BadRequest(ModelState);
            }
            var carModel = carDto.ToCarFromCreateDTO();
            await _carRepository.CreateCar(carModel);
            return CreatedAtAction(nameof(GetCarById), new { id = carModel.Id }, carModel.ToCarDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCar([FromRoute] int id, [FromBody] UpdateCarRequestDto updateCarDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var carModel = await _carRepository.UpdateCar(id, updateCarDto);

            if (carModel == null)
            {
                return NotFound();
            }

            return Ok(carModel.ToCarDto());

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var carModel = await _carRepository.DeleteCar(id);

            if (carModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}