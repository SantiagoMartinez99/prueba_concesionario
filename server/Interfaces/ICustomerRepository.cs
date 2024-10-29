using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Models;

namespace server.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomers();
        Task<Customer?> GetCustomerById(int id);

        Task<Customer> CreateCustomer(Customer customerModel);
    }
}