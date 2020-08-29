using System.Threading.Tasks;
using WebApplication1.Core;
using WebApplication1.Core.Models;

namespace WebApplication1.Presistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public VegaDbContext Context { get; set; }
        public UnitOfWork(VegaDbContext context)
        {
            this.Context = context;
        }


        public async Task CompleteAsync()
        {
            await Context.SaveChangesAsync();
        }
    }

}