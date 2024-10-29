using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Dtos.Vehicle
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Mileage { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public string Image { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public bool IsUsed { get; set; } = false;
        public int EngineCapacity { get; set; } = 0;
        public int Gears { get; set; } = 0;
    }
}