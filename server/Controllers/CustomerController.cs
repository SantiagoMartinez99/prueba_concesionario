using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Dtos.Customer;
using server.Interfaces;
using server.Mappers;

namespace server.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ApplicationDBContext context, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customerModel = await _customerRepository.GetAllCustomers();
            var customerDto = customerModel.Select(v => v.ToCustomerDto());

            return Ok(customerModel);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customerModel = await _customerRepository.GetCustomerById(id);

            if (customerModel == null)
            {
                return NotFound();
            }

            return Ok(customerModel.ToCustomerDto());
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequestDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customerModel = customerDto.ToCustomerFromCreateDTO();
            await _customerRepository.CreateCustomer(customerModel);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customerModel.Id }, customerModel.ToCustomerDto());
        }

    }
}