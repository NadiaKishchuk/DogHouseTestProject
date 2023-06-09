using DogsHouse.DAL.Entities;
using DogsHouse.DAL.Persistance;
using DogsHouse.DAL.Repositories.Base;

namespace DogsHouse.DAL.Repositories.Dogs
{
    public class DogRepository : RepositoryBase<Dog>, IDogRepository
    {
        public DogRepository(DogsHouseDBContext context)
            : base(context)
        {
        }
    }
}
