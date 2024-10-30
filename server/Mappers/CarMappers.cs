using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Dtos.Car;
using server.Models;

namespace server.Mappers
{
    public static class CarMappers
    {
        public static CarDto ToCarDto(this Car carModel)
        {
            return new CarDto
            {
                Id = carModel.Id,
                Model = carModel.Model,
                Color = carModel.Color,
                Mileage = carModel.Mileage,
                Price = carModel.Price,
                Image = carModel.Image,
                RegistrationDate = carModel.RegistrationDate,
                IsUsed = carModel.IsUsed,
                IsSold = carModel.IsSold
            };
        }

        public static Car ToCarFromCreateDTO(this CreateCarRequestDto carDto)
        {
            return new Car
            {
                Model = carDto.Model,
                Color = carDto.Color,
                Mileage = carDto.Mileage,
                Price = carDto.Price,
                Image = carDto.Image,
                RegistrationDate = carDto.RegistrationDate,
                IsUsed = carDto.IsUsed,
                IsSold = carDto.IsSold
            };
        }
    }
}