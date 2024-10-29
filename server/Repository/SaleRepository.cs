using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Interfaces;
using server.Models;

namespace server.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDBContext _context;

        public SaleRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Sale> CreateSale(Sale saleModel)
        {
            await _context.Sale.AddAsync(saleModel);
            await _context.SaveChangesAsync();
            return saleModel;
        }

        public async Task<List<Sale>> GetAllSales()
        {
            return await _context.Sale.ToListAsync();
        }

        public async Task<Sale?> GetSaleById(int id)
        {
            return await _context.Sale.FindAsync(id);
        }
    }
}