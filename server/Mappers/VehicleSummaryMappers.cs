using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using server.Dtos.VehicleSummary;
using server.Models;

namespace server.Mappers
{
    public static class VehicleSummaryMappers
    {
        public static VehicleSummaryDto ToVehicleSummaryDto(this VehicleSummary vehicleSummaryModel, int count)
        {
            return new VehicleSummaryDto
            {
                Model = vehicleSummaryModel.Model,
                TotalValue = vehicleSummaryModel.TotalValue,
                VehicleCount = count,
                IsSold = vehicleSummaryModel.IsSold

            };
        }
    }
}