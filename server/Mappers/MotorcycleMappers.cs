using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Dtos.Motorcycle;
using server.Models;

namespace server.Mappers
{
    public static class MotorcycleMappers
    {
        public static MotorcycleDto ToMotorcycleDto(this Motorcycle motorcycleModel)
        {
            return new MotorcycleDto
            {
                Id = motorcycleModel.Id,
                Model = motorcycleModel.Model,
                Color = motorcycleModel.Color,
                Mileage = motorcycleModel.Mileage,
                Price = motorcycleModel.Price,
                Image = motorcycleModel.Image,
                RegistrationDate = motorcycleModel.RegistrationDate,
                IsUsed = motorcycleModel.IsUsed,
                EngineCapacity = motorcycleModel.EngineCapacity,
                Gears = motorcycleModel.Gears,
                IsSold = motorcycleModel.IsSold


            };
        }
        public static Motorcycle ToMotorcycleFromCreateDto(this CreateMotorcycleRequestDto motorcycleDto)
        {
            return new Motorcycle
            {
                Model = motorcycleDto.Model,
                Color = motorcycleDto.Color,
                Mileage = motorcycleDto.Mileage,
                Price = motorcycleDto.Price,
                Image = motorcycleDto.Image,
                RegistrationDate = motorcycleDto.RegistrationDate,
                IsUsed = motorcycleDto.IsUsed,
                EngineCapacity = motorcycleDto.EngineCapacity,
                Gears = motorcycleDto.Gears,
                IsSold = motorcycleDto.IsSold

            };
        }
    }
}