using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApplication1.Core.Models;
using WebApplication1.Presistence;

namespace WebApplication1.Core
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork unitOfWork;

        public PhotoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Photo> UploadPhoto(Vehicle vehicle, IFormFile file, string uploadFolderPath)
        {
            if(!Directory.Exists(uploadFolderPath))
               Directory.CreateDirectory(uploadFolderPath);
            var fileName  = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath =  Path.Combine(uploadFolderPath,fileName);
            using(var stream = new FileStream(filePath,FileMode.Create))
            {
               await file.CopyToAsync(stream);
            }
            var photo = new Photo(){ FileName = fileName};
            vehicle.Photos.Add(photo);
            await unitOfWork.CompleteAsync();
            return photo;
        }
    }
}