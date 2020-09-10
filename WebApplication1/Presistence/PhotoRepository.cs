using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Core;
using WebApplication1.Core.Models;

namespace WebApplication1.Presistence
{
     [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotoRepository : IPhotoRepository
    {
        private readonly VegaDbContext context;

        public PhotoRepository(VegaDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
        {
            return  await context.Photos.Where(ph => ph.VehicleId == vehicleId)
            .ToListAsync();

        }
    }
}