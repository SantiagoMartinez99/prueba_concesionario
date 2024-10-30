using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace server.Dtos.Motorcycle
{
    public class CreateMotorcycleRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Car Model can not have more than {100} characters")]
        [MinLength(2, ErrorMessage = "Car Model can not have less than {2} characters")]
        public string Model { get; set; } = string.Empty;
        [Required]
        [MaxLength(100, ErrorMessage = "Car Color can not have more than {100} characters")]
        [MinLength(2, ErrorMessage = "Car Color can not have less than {2} characters")]

        public string Color { get; set; } = string.Empty;
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Car Mileage can not be negative")]
        public int Mileage { get; set; } = 0;
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Car Price can not be negative")]
        public decimal Price { get; set; } = 0;
        public string Image { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        [Required]
        public bool IsUsed { get; set; } = false;
        [Required]
        public int EngineCapacity { get; set; } = 0;
        [Required]
        public int Gears { get; set; } = 0;

        public bool IsSold { get; set; } = false;

    }
}