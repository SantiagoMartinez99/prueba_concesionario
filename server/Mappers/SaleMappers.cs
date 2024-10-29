using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Dtos.Sale;
using server.Models;

namespace server.Mappers
{
    public static class SaleMappers
    {
        public static SaleDto ToSaleDto(this Sale saleModel)
        {
            return new SaleDto
            {
                Id = saleModel.Id,
                VehicleId = saleModel.VehicleId,
                CustomerId = saleModel.CustomerId,
                SaleDate = saleModel.SaleDate,

            };
        }
        public static Sale ToSaleFromCreateDto(this CreateSaleRequestDto saleDto)
        {
            return new Sale
            {
                VehicleId = saleDto.VehicleId,
                CustomerId = saleDto.CustomerId,
                SaleDate = saleDto.SaleDate

            };
        }
    }
}