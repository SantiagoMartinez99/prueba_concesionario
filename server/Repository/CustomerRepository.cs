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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDBContext _context;
        public CustomerRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Customer> CreateCustomer(Customer customerModel)
        {
            await _context.Customer.AddAsync(customerModel);
            await _context.SaveChangesAsync();
            return customerModel;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _context.Customer.ToListAsync();
        }

        public async Task<Customer?> GetCustomerById(int id)
        {
            return await _context.Customer.FindAsync(id);
        }
    }
}