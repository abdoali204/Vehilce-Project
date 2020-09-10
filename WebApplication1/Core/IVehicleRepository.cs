using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Core.Models;

namespace WebApplication1.Core
{
    public interface IVehicleRepository
    {

     Task<Vehicle> GetVehicleAsync(int id,bool includeRelated = true);
     void Add(Vehicle vehicle);
     void Remove(Vehicle vehicle);
     Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObj);
    }
}