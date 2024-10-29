using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public int? VehicleId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,2)")]
        public Customer? Customer { get; set; }
    }
}