using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;

namespace server.Interfaces
{
    public interface IVehicleSummaryRepository
    {
        Task<IEnumerable<VehicleSummary>> GetVehicleSummary();
    }
}