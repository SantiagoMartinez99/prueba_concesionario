using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Dtos.Vehicle;
using server.Models;

namespace server.Mappers
{
    public static class VehicleMappers
    {
        public static VehicleDto ToVehicleDto(this Vehicle vehicleModel)
        {
            return new VehicleDto
            {
                Id = vehicleModel.Id,
                Model = vehicleModel.Model,
                Color = vehicleModel.Color,
                Mileage = vehicleModel.Mileage,
                Price = vehicleModel.Price,
                Image = vehicleModel.Image,
                RegistrationDate = vehicleModel.RegistrationDate,
                IsUsed = vehicleModel.IsUsed,
                IsSold = vehicleModel.IsSold
            };
        }
    }
}