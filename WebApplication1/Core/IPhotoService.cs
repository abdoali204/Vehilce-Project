using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApplication1.Core.Models;
using WebApplication1.Presistence;

namespace WebApplication1.Core
{
    public interface IPhotoService
    {
        Task<Photo> UploadPhoto(Vehicle vehicle,IFormFile file,string uploadFolderPath);
    
    }
}