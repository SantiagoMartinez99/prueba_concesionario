using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Dtos.VehicleSummary
{
    public class VehicleSummaryDto
    {
        public string? Model { get; set; }
        public decimal TotalValue { get; set; }
        public int VehicleCount { get; set; }
    }
}