using DogsHouse.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DogsHouse.DAL.Persistance
{
    public class DogsHouseDBContext: DbContext
    {
        public DbSet<Dog> Dogs { get; set; }

        public DogsHouseDBContext()
        {
        }

        public DogsHouseDBContext(DbContextOptions<DogsHouseDBContext> options)
            : base(options)
        {
        }
    }
}
