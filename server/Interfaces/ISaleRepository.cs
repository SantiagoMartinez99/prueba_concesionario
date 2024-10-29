using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Models;

namespace server.Interfaces
{
    public interface ISaleRepository
    {
        Task<List<Sale>> GetAllSales();

        Task<Sale> CreateSale(Sale saleModel);

        Task<Sale?> GetSaleById(int id);
    }
}