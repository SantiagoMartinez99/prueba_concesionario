using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Dtos.Car;
using server.Models;

namespace server.Interfaces
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAllCars();

        Task<Car?> GetCarById(int id);

        Task<Car> CreateCar(Car carModel);

        Task<Car?> UpdateCar(int id, UpdateCarRequestDto updateCarDto);

        Task<Car?> DeleteCar(int id);
        Task<int> GetTotalCarCountAsync();
    }
}