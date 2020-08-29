using System.Threading.Tasks;

namespace WebApplication1.Presistence
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }

}