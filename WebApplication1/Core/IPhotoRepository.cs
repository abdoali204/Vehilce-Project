using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Core.Models;

namespace WebApplication1.Core
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int vehicleId);   
    }
}