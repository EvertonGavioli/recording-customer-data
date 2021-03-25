using System.Threading.Tasks;

namespace RCD.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
