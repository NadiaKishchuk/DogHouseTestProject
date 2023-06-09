using DogsHouse.BLL.DTO.Dogs;
using System.Diagnostics.CodeAnalysis;

namespace DogsHouse.Tests.Utils
{
    internal class NameEqualityComparer : IEqualityComparer<DogDTO>
    {
        public bool Equals(DogDTO? x, DogDTO? y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.Name.Equals(y.Name);
        }

        public int GetHashCode([DisallowNull] DogDTO obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
