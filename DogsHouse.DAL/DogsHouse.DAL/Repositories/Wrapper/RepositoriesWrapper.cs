using DogsHouse.DAL.Persistance;
using DogsHouse.DAL.Repositories.Dogs;

namespace DogsHouse.DAL.Repositories.Wrapper
{
    public class RepositoriesWrapper : IRepositoriesWrapper
    {
        private readonly DogsHouseDBContext _dogsHouseDbContext;
        public RepositoriesWrapper(DogsHouseDBContext dogsHouseDbContext)
        {
            if (dogsHouseDbContext != null)
            {
                _dogsHouseDbContext = dogsHouseDbContext;
            }
            else
            {
                throw new ArgumentNullException(nameof(dogsHouseDbContext));
            }
        }

        private IDogRepository _dogRepository;
        public IDogRepository DogRepository {
            get
            {
                _dogRepository ??= new DogRepository(_dogsHouseDbContext);

                return _dogRepository;
            }
        }

        public int SaveChanges()
        {
            return _dogsHouseDbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _dogsHouseDbContext.SaveChangesAsync();
        }
    }
}
