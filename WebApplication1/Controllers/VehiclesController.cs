using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Core.Models;
using WebApplication1.Core;
using WebApplication1.Resources;
using WebApplication1.Presistence;

namespace WebApplication1.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepository repositry;
        private readonly IUnitOfWork unitOfWork;

        public VehiclesController(IMapper mapper , IVehicleRepository repositry,IUnitOfWork unitOfWork )
        {
            this.mapper = mapper;
            this.repositry = repositry;
            this.unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            repositry.Add(vehicle);
            await unitOfWork.CompleteAsync();

            vehicle = await repositry.GetVehicleAsync(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpPut("{id}")] 
    public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
    {
        if (!ModelState.IsValid)
          return BadRequest(ModelState);

        var vehicle = await repositry.GetVehicleAsync(id);

        if (vehicle == null)
          return NotFound();

        mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
        vehicle.LastUpdate = DateTime.Now;

        await unitOfWork.CompleteAsync();
        vehicle = await repositry.GetVehicleAsync(vehicle.Id);
        var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        var vehicle =await repositry.GetVehicleAsync(id,includeRelated : false);
        if(vehicle == null)
            return NotFound();
       repositry.Remove(vehicle);
        await unitOfWork.CompleteAsync();
        return Ok(id);

    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicle(int id)
    {
         if(!ModelState.IsValid)
            return BadRequest(ModelState);
        var vehicle =await repositry.GetVehicleAsync(id);
        if(vehicle == null)
            return NotFound();
        var vehicleResource = Mapper.Map<Vehicle,VehicleResource>(vehicle);
        return Ok(vehicleResource);       
    }
    }
}