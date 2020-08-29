using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Core;
using WebApplication1.Core.Models;

namespace WebApplication1.Presistence
{
    public class VehicleRepositry : IVehicleRepository
    {
        
        public VegaDbContext VegaDbContext { get; }
        public VehicleRepositry(VegaDbContext vegaDbContext)
        {
            VegaDbContext = vegaDbContext;
        }


        public async Task<Vehicle> GetVehicleAsync(int id,bool inCludeRelated = true)
        {  
            if(!inCludeRelated)
                return await VegaDbContext.Vehicles.FindAsync(id);
           return await VegaDbContext.Vehicles
            .Include(v=> v.Features)
            .ThenInclude(vf=> vf.Feature)
            .Include(v=>v.Model)
            .ThenInclude(m=>m.Make)
            .SingleOrDefaultAsync(v=> v.Id ==  id);
        }

        public void Add(Vehicle vehicle)
        {
            VegaDbContext.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {  
            VegaDbContext.Vehicles.Remove(vehicle);
        }
    }
}