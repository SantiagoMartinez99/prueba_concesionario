using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Interfaces;
using server.Models;

namespace server.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDBContext _context;

        public VehicleRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Vehicle>> GetAllVehicles()
        {
            return await _context.Vehicle.ToListAsync();
        }

    }

}