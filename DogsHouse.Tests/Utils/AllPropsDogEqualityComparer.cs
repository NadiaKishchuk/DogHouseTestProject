using DogsHouse.BLL.DTO.Dogs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouse.Tests.Utils
{
    public class AllPropsDogEqualityComparer : IEqualityComparer<DogDTO>
    {
        public bool Equals(DogDTO? x, DogDTO? y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.Name.Equals(y.Name)  && x.tail_length == y.tail_length 
                && x.Color.Equals(y.Color) && x.Weight == y.Weight;
        }

        public int GetHashCode([DisallowNull] DogDTO obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
