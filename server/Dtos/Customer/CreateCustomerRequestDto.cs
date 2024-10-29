using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace server.Dtos.Customer
{
    public class CreateCustomerRequestDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int DocumentNumber { get; set; } = 0;
    }
}