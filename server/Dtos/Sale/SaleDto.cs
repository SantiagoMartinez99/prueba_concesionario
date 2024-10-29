using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Dtos.Sale
{
    public class SaleDto
    {
        public int Id { get; set; }

        public int? VehicleId { get; set; }

        public int? CustomerId { get; set; }

        public DateTime SaleDate { get; set; }
    }
}