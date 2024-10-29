using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Mileage { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } = 0;
        public string Image { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public bool IsUsed { get; set; } = false;
    }
}