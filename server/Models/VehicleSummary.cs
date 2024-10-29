using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public class VehicleSummary
    {
        public string? Model { get; set; }
        public decimal TotalValue { get; set; }
        public int VehicleCount { get; set; }
    }
}