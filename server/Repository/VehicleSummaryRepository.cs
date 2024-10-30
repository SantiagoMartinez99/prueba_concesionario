using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Interfaces;
using server.Models;

namespace server.Repository
{
    public class VehicleSummaryRepository : IVehicleSummaryRepository
    {
        private readonly ApplicationDBContext _context;
        public VehicleSummaryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehicleSummary>> GetVehicleSummary()
        {
            var summary = await _context.Vehicle
                .GroupBy(v => v.Model)
                .Select(g => new VehicleSummary
                {
                    Model = g.Key,
                    TotalValue = g.Sum(v => v.Price),
                    IsSold = g.Any(v => v.IsSold),
                    VehicleCount = g.Count()

                })
                .ToListAsync();

            return summary;
        }
    }
}