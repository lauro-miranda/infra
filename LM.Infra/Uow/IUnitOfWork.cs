using System.Threading.Tasks;

namespace LM.Infra.Data
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}
