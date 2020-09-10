using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using WebApplication1.Core.Models;
using WebApplication1.Core;
using WebApplication1.Presistence;
using AutoMapper;
using WebApplication1.Resources;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IWebHostEnvironment hostEnv;
        private readonly IVehicleRepository repository;
        private readonly IMapper mapper;
        private readonly PhotoSettings photoSettings;
        private readonly IOptionsSnapshot<PhotoSettings> options;
        private readonly IPhotoRepository photoRepository;
        private readonly IPhotoService photoService;

        public PhotosController(IWebHostEnvironment hostEnv,IVehicleRepository repository,
                                IMapper mapper,
                                IOptionsSnapshot<PhotoSettings> options,
                                IPhotoRepository photoRepository,
                                IPhotoService photoService)
        {
            this.photoSettings = options.Value;
            this.options = options;
            this.photoRepository = photoRepository;
            this.photoService = photoService;
            this.hostEnv = hostEnv;
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> UploadAsync(int vehicleId,[FromForm(Name="file")]IFormFile file)
        {
            var vehicle =  await repository.GetVehicleAsync(vehicleId);
            if(vehicle == null)
                return NotFound();
                //conditions
            if(file == null)
                return BadRequest("Null File");
            if(file.Length == 0)
                return BadRequest("Empty File");
            if(file.Length > photoSettings.MaxBytes)
                return BadRequest("Exceeded File Size");
            if(!photoSettings.IsSupported(file.FileName))
                return BadRequest("Not Supported File");

            var uploadFolderPath =  Path.Combine(hostEnv.WebRootPath,"uploads");
            var photo = await photoService.UploadPhoto(vehicle,file,uploadFolderPath);

            return Ok(mapper.Map<Photo,PhotoResource>(photo));
        }
        public async Task<IEnumerable<PhotoResource>> GetPhotos(int vehicleId)
        {
            var photos =await photoRepository.GetPhotos(vehicleId);
            return mapper.Map<IEnumerable<Photo>,IEnumerable<PhotoResource>>(photos);

        }

    }
}
