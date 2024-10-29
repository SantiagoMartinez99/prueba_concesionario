using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public class Motorcycle : Vehicle
    {
        public int EngineCapacity { get; set; }
        public int Gears { get; set; } 
    }
}