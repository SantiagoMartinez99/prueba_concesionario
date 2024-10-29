using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using server.Data;
using server.Dtos.VehicleSummary;
using server.Interfaces;
using server.Mappers;
using server.Models;

namespace server.Controllers
{
    [Route("api/vehiclesummary")]
    [ApiController]
    public class VehicleSummaryController : ControllerBase
    {
        private readonly IVehicleSummaryRepository _vehicleSummaryRepository;

        public VehicleSummaryController(IVehicleSummaryRepository vehicleSummaryRepository)
        {
            _vehicleSummaryRepository = vehicleSummaryRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleSummary>>> GetVehicleSummary()
        {
            var summary = await _vehicleSummaryRepository.GetVehicleSummary();

            if (summary == null || !summary.Any())
            {
                return NotFound();
            }

            return Ok(summary);
        }
    }
}