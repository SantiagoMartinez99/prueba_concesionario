using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Dtos.Customer;
using server.Models;

namespace server.Mappers
{
    public static class CustomerMappers
    {
        public static CustomerDto ToCustomerDto(this Customer customerModel)
        {
            return new CustomerDto
            {
                Id = customerModel.Id,
                Name = customerModel.Name,
                DocumentNumber = customerModel.DocumentNumber,
            };
        }
        public static Customer ToCustomerFromCreateDTO(this CreateCustomerRequestDto customerDto)
        {
            return new Customer
            {
                Name = customerDto.Name,
                DocumentNumber = customerDto.DocumentNumber,
            };
        }
    }
}