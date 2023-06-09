using DogsHouse.BLL.DTO.AdditionalRequestDTO;
using DogsHouse.BLL.DTO.Dogs;

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
            new object[] {"name", OrderEnum.Asc, CompareByNameAsc},
            new object[] {"Name", OrderEnum.Asc, CompareByNameAsc},
            new object[] {"naMe", OrderEnum.Asc, CompareByNameAsc},
            new object[] {"name", OrderEnum.Desc, CompareByNameDesc},
            new object[] {"Name", OrderEnum.Desc, CompareByNameDesc},
            new object[] {"naMe", OrderEnum.Desc, CompareByNameDesc},

            new object[] {"weight", OrderEnum.Asc, CompareByWeightAsc},
            new object[] {"weight", OrderEnum.Desc, CompareByWeightDesc},
            new object[] {"weIght", OrderEnum.Asc, CompareByWeightAsc},
            new object[] {"weighT", OrderEnum.Desc, CompareByWeightDesc},


            new object[] { "tail_length", OrderEnum.Asc, CompareByTailLengthAsc},
            new object[] { "tail_length", OrderEnum.Desc, CompareByTailLengthDesc},
            new object[] { "tailleNgth", OrderEnum.Asc, CompareByTailLengthAsc},
            new object[] { "Taillength", OrderEnum.Desc, CompareByTailLengthDesc},

            new object[] { "Color", OrderEnum.Asc, CompareByColorAsc},
            new object[] { "Color", OrderEnum.Desc, CompareByColorDesc},
            new object[] { "color", OrderEnum.Asc, CompareByColorAsc},
            new object[] { "color", OrderEnum.Desc, CompareByColorDesc},

        };
    }
}
