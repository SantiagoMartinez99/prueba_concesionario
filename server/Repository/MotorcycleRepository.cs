using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Dtos.Motorcycle;
using server.Interfaces;
using server.Models;

namespace server.Repository
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly ApplicationDBContext _context;
        public MotorcycleRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Motorcycle>> GetAllMotorcycles()
        {
            return await _context.Motorcycle.ToListAsync();
        }

        public async Task<Motorcycle?> GetMotorcycleById(int id)
        {
            return await _context.Motorcycle.FindAsync(id);
        }

        public async Task<Motorcycle> CreateMotorcycle(Motorcycle motorcycle)
        {
            await _context.Motorcycle.AddAsync(motorcycle);
            await _context.SaveChangesAsync();
            return motorcycle;
        }

        public async Task<Motorcycle?> DeleteMotorcycle(int id)
        {
            var motorcycle = await _context.Motorcycle.FirstOrDefaultAsync(v => v.Id == id);
            if (motorcycle == null)
            {
                return null;
            }
            _context.Motorcycle.Remove(motorcycle);
            await _context.SaveChangesAsync();
            return motorcycle;
        }

        public async Task<int> CreateMotorcycle(Car carModel)
        {
            return await _context.Motorcycle.CountAsync();
        }

        public async Task<Motorcycle?> UpdateMotorcycle(int id, UpdateMotorcycleRequestDto updateMotorcycleDto)
        {
            var motorcycle = await _context.Motorcycle.FirstOrDefaultAsync(v => v.Id == id);
            if (motorcycle == null)
            {
                return null;
            }
            motorcycle.Model = updateMotorcycleDto.Model;
            motorcycle.Color = updateMotorcycleDto.Color;
            motorcycle.Mileage = updateMotorcycleDto.Mileage;
            motorcycle.Price = updateMotorcycleDto.Price;
            motorcycle.Image = updateMotorcycleDto.Image;
            motorcycle.RegistrationDate = updateMotorcycleDto.RegistrationDate;
            motorcycle.IsUsed = updateMotorcycleDto.IsUsed;
            motorcycle.EngineCapacity = updateMotorcycleDto.EngineCapacity;
            motorcycle.Gears = updateMotorcycleDto.Gears;
            motorcycle.IsSold = updateMotorcycleDto.IsSold;
            await _context.SaveChangesAsync();
            return motorcycle;
        }

        public async Task<int> GetTotalMotorcycleCountAsync()
        {
            return await _context.Motorcycle.CountAsync();
        }
    }
}