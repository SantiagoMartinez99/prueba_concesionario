using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Models;

namespace server.Interfaces
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetAllVehicles();
    }
}