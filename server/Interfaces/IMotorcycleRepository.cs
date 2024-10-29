using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Dtos.Motorcycle;
using server.Models;

namespace server.Interfaces
{
    public interface IMotorcycleRepository
    {
        Task<List<Motorcycle>> GetAllMotorcycles();

        Task<Motorcycle?> GetMotorcycleById(int id);

        Task<Motorcycle> CreateMotorcycle(Motorcycle motorcycleModel);

        Task<Motorcycle?> UpdateMotorcycle(int id, UpdateMotorcycleRequestDto updateMotorcycleDto);

        Task<Motorcycle?> DeleteMotorcycle(int id);
        Task<int> GetTotalMotorcycleCountAsync();
    }
}