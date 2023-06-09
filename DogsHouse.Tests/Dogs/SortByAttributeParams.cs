using DogsHouse.BLL.DTO.AdditionalRequestDTO;
using DogsHouse.BLL.DTO.Dogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouse.Tests.Dogs
{
    public class SortByAttributeParams
    {
        private static Func<DogDTO, DogDTO, bool> CompareByNameAsc = (d1, d2) => d1.Name.CompareTo(d2.Name) <= 0;
        private static Func<DogDTO, DogDTO, bool> CompareByNameDesc = (DogDTO d1, DogDTO d2) => d1.Name.CompareTo(d2.Name) >= 0;

        private static Func<DogDTO, DogDTO, bool> CompareByColorAsc = (d1, d2) => d1.Color.CompareTo(d2.Color) <= 0;
        private static Func<DogDTO, DogDTO, bool> CompareByColorDesc = (DogDTO d1, DogDTO d2) => d1.Color.CompareTo(d2.Color) >= 0;

        private static Func<DogDTO, DogDTO, bool> CompareByWeightAsc = (d1, d2) => d1.Weight <= d2.Weight;
        private static Func<DogDTO, DogDTO, bool> CompareByWeightDesc = (DogDTO d1, DogDTO d2) => d1.Weight >= d2.Weight;


        private static Func<DogDTO, DogDTO, bool> CompareByTailLengthAsc = (d1, d2) => d1.tail_length <= d2.tail_length;
        private static Func<DogDTO, DogDTO, bool> CompareByTailLengthDesc = (DogDTO d1, DogDTO d2) => d1.tail_length >= d2.tail_length;

        public static IEnumerable<object[]> TestCases => new List<object[]>()
                {
            new object[] {"name", OrderEnum.Ascen, CompareByNameAsc},
            new object[] {"Name", OrderEnum.Ascen, CompareByNameAsc},
            new object[] {"naMe", OrderEnum.Ascen, CompareByNameAsc},
            new object[] {"name", OrderEnum.Desc, CompareByNameDesc},
            new object[] {"Name", OrderEnum.Desc, CompareByNameDesc},
            new object[] {"naMe", OrderEnum.Desc, CompareByNameDesc},

            new object[] {"weight", OrderEnum.Ascen, CompareByWeightAsc},
            new object[] {"weight", OrderEnum.Desc, CompareByWeightDesc},
            new object[] {"weIght", OrderEnum.Ascen, CompareByWeightAsc},
            new object[] {"weighT", OrderEnum.Desc, CompareByWeightDesc},


            new object[] { "tail_length", OrderEnum.Ascen, CompareByTailLengthAsc},
            new object[] { "tail_length", OrderEnum.Desc, CompareByTailLengthDesc},
            new object[] { "tailleNgth", OrderEnum.Ascen, CompareByTailLengthAsc},
            new object[] { "Taillength", OrderEnum.Desc, CompareByTailLengthDesc},

            new object[] { "Color", OrderEnum.Ascen, CompareByColorAsc},
            new object[] { "Color", OrderEnum.Desc, CompareByColorDesc},
            new object[] { "color", OrderEnum.Ascen, CompareByColorAsc},
            new object[] { "color", OrderEnum.Desc, CompareByColorDesc},

        };
    }
}
