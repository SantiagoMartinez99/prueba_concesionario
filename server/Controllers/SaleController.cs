using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Dtos.Sale;
using server.Interfaces;
using server.Mappers;

namespace server.Controllers
{
    [Route("api/sale")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ISaleRepository _saleRepository;
        public SaleController(ApplicationDBContext context, ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSales()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var saleModels = await _saleRepository.GetAllSales();
            var saleDtos = saleModels.Select(s => s.ToSaleDto());
            return Ok(saleDtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSaleById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var saleModel = await _saleRepository.GetSaleById(id);
            if (saleModel == null)
            {
                return NotFound();
            }

            return Ok(saleModel.ToSaleDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequestDto saleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var saleModel = saleDto.ToSaleFromCreateDto();
            await _saleRepository.CreateSale(saleModel);

            return CreatedAtAction(nameof(GetSaleById), new { id = saleModel.Id }, saleModel.ToSaleDto());
        }
    }
}