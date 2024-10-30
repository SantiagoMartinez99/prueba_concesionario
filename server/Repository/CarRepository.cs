using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Dtos.Car;
using server.Interfaces;
using server.Models;

namespace server.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDBContext _context;
        public CarRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Car> CreateCar(Car carModel)
        {
            await _context.Car.AddAsync(carModel);
            await _context.SaveChangesAsync();
            return carModel;
        }

        public async Task<Car?> DeleteCar(int id)
        {
            var carModel = await _context.Car.FirstOrDefaultAsync(v => v.Id == id);
            if (carModel == null)
            {
                return null;
            }
            _context.Car.Remove(carModel);
            await _context.SaveChangesAsync();
            return carModel;
        }

        public async Task<List<Car>> GetAllCars()
        {
            return await _context.Car.ToListAsync();
        }

        public async Task<Car?> GetCarById(int id)
        {
            return await _context.Car.FindAsync(id);
        }

        public async Task<int> GetTotalCarCountAsync()
        {
            return await _context.Car.CountAsync();
        }

        public async Task<Car?> UpdateCar(int id, UpdateCarRequestDto updateCarDto)
        {
            var carModel = await _context.Car.FirstOrDefaultAsync(v => v.Id == id);
            if (carModel == null)
            {
                return null;
            }
            carModel.Model = updateCarDto.Model;
            carModel.Color = updateCarDto.Color;
            carModel.Mileage = updateCarDto.Mileage;
            carModel.Price = updateCarDto.Price;
            carModel.Image = updateCarDto.Image;
            carModel.RegistrationDate = updateCarDto.RegistrationDate;
            carModel.IsUsed = updateCarDto.IsUsed;
            carModel.IsSold = updateCarDto.IsSold;
            await _context.SaveChangesAsync();

            return carModel;
        }
    }
}