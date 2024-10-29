using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace server.Dtos.Sale
{
    public class CreateSaleRequestDto
    {
        [Required]
        public int? VehicleId { get; set; }
        [Required]


        public int? CustomerId { get; set; }
        [Required]


        public DateTime SaleDate { get; set; }
    }
}