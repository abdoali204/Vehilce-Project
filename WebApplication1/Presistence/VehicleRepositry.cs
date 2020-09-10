using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Core;
using WebApplication1.Core.Models;
using WebApplication1.Extentions;
using WebApplication1.Resources;

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
        public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObj)
        {
            var result = new QueryResult<Vehicle>();
            var columnsMap = new Dictionary<string,Expression<Func<Vehicle,object>>>()
            {
                ["make"] = v=> v.Model.Make.Name,
                ["model"] = v=> v.Model.Name,
                ["contactName"] = v => v.ConcatName
            };

            var query =  VegaDbContext.Vehicles
                        .Include(v=>v.Model)
                            .ThenInclude(m=>m.Make).AsQueryable();

                query = query.ApplyFiltering(queryObj);

                query = query.ApplyOrdering(queryObj,columnsMap);
                result.TotalItems = await query.CountAsync();

            query = query.Paging(queryObj);
            result.Items =await query.ToListAsync();
            return result;
                    
        }
    }
}