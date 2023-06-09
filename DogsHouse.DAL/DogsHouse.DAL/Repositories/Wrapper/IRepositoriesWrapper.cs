
using DogsHouse.DAL.Repositories.Dogs;

namespace DogsHouse.DAL.Repositories.Wrapper
{
    public interface IRepositoriesWrapper
    {
        IDogRepository DogRepository { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
