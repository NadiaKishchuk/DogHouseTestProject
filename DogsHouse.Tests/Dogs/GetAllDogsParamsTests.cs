using AutoMapper;
using DogsHouse.BLL.DTO.AdditionalRequestDTO;
using DogsHouse.BLL.DTO.Dogs;
using DogsHouse.BLL.MediatR.Dogs.GetAllWithParams;
using DogsHouse.DAL.Entities;
using DogsHouse.DAL.Repositories.Wrapper;
using DogsHouse.Tests.Utils;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace DogsHouse.Tests.Dogs
{
    public class GetAllDogsParamsTests
    {
        private readonly Mock<IRepositoriesWrapper> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;

        public GetAllDogsParamsTests()
        {
            _mockRepo = new Mock<IRepositoriesWrapper>();
            _mockMapper = new Mock<IMapper>();
        }

        void SetupRepository(List<Dog> returnList)
        {
            _mockRepo.Setup(repo => repo.DogRepository.GetQueryable(
                  It.IsAny<Expression<Func<Dog, bool>>>(),
                It.IsAny<Func<IQueryable<Dog>, IIncludableQueryable<Dog, object>>>(),
                It.IsAny<Expression<Func<Dog, Dog>>>())).Returns(returnList.AsQueryable());
        }

        void SetupMapper()
        {
            _mockMapper.Setup(x => x.Map<IEnumerable<DogDTO>>(It.IsAny<IEnumerable<Dog>>()))
                .Returns<IEnumerable<Dog>>(arr => arr.Select(d => 
                new DogDTO() { Name = d.Name, Color = d.Color, tail_length = d.TailLength, Weight = d.Weight }));
        }
        private readonly List<Dog> dogs = new List<Dog>()
        {
            new Dog
            {
                Color = "red",
                Name = "Jenny",
                TailLength = 3,
                Weight = 5
            },
            new Dog
            {
                Color = "bedge",
                Name = "Mayk",
                TailLength = 13,
                Weight = 53
            }
        };
        private readonly List<DogDTO> dogsDTO = new List<DogDTO>()
        {
            new DogDTO
            {
                Color = "red",
                Name = "Jenny",
                tail_length = 3,
                Weight = 5
            },
            new DogDTO
            {
                Color = "bedge",
                Name = "Mayk",
                tail_length = 13,
                Weight = 53
            }
        };

        [Fact]
        public async Task Handler_GetWithoutParams_Returns_AllList()
        {
            //Arrange
            SetupRepository(dogs);
            SetupMapper();

            var handler = new GetAllDogsWithParamsHandler(_mockRepo.Object, _mockMapper.Object);

            //Act
            var result = await handler.Handle(new GetAllDogsWithParamsQuery(null, null, null, null), CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Multiple(
                () => Assert.True(result.Value.All((d) => (d is DogDTO))),
                () => Assert.True(result.Value.Count() == dogs.Count));
        }

        [Theory]
        [MemberData(nameof(SortByAttributeParams.TestCases), MemberType = typeof(SortByAttributeParams))]
        public async Task Handler_GetDogsName_CaseInsensative_Returns_SortedList(string sortAttribute, OrderEnum order, Func<DogDTO, DogDTO, bool> compareFunc)
        {
            //Arrange
            SetupRepository(dogs);
            SetupMapper();

            var handler = new GetAllDogsWithParamsHandler(_mockRepo.Object, _mockMapper.Object);

            //Act
            var result = await handler.Handle(new GetAllDogsWithParamsQuery(sortAttribute, order, null, null), CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Multiple(
                () => Assert.True(result.Value.All((d) => (d is DogDTO))),
                () => Assert.True(result.Value.Count() == dogs.Count));


            Assert.True(IsSorted(result.Value, compareFunc));
        }

        [Theory]
        [InlineData(1 ,2)]
        [InlineData(2, 1)]
        [InlineData(1, 1)]
        [InlineData(-2, 1)]
        public async Task Handler_GetDogsPageLimit_Returns_DogsListPage(int page, int limit)
        {
            //Arrange
            SetupRepository(dogs);
            SetupMapper();
            var returnedArray = dogsDTO.Skip(page - 1).Take(limit).ToList();

            var handler = new GetAllDogsWithParamsHandler(_mockRepo.Object, _mockMapper.Object);

            //Act
            var result = await handler.Handle(new GetAllDogsWithParamsQuery(null, null, page, limit), CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.ValueOrDefault);
            Assert.Multiple(
                () => Assert.True(result.Value.All((d) => (d is DogDTO))),
                () => Assert.True(result.Value.Count() == limit),
                () => Assert.Equal(result.Value,returnedArray, new NameEqualityComparer()));
        }

        [Theory]
        [InlineData("name" ,OrderEnum.Desc, null,1)]
        [InlineData(null, null, 1, null)]
        [InlineData("name", null, null, null)]
        [InlineData(null, OrderEnum.Desc, null, null)]

        [InlineData(null, null, 1, 1)]
        [InlineData("name", OrderEnum.Desc, null, null)]
        [InlineData("name", OrderEnum.Desc, 1, null)]
        [InlineData("name", null, 1, null)]
        [InlineData(null, OrderEnum.Desc, 1, null)]
        [InlineData(null, OrderEnum.Desc, 1, 1)]
        public async Task Handler_GetDogsParams_WithNullValues_Returns_SortedList(string? attribute,
            OrderEnum? order, int? pageNumber, int? limit)
        {
            //Arrange
            SetupRepository(dogs);
            SetupMapper();

            var handler = new GetAllDogsWithParamsHandler(_mockRepo.Object, _mockMapper.Object);

            //Act
            var result = await handler.Handle(new GetAllDogsWithParamsQuery(attribute, order, pageNumber, limit), CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.ValueOrDefault);
            Assert.Multiple(
                () => Assert.True(result.Value.All((d) => (d is DogDTO))),
                () => Assert.True(result.Value.Count() == limit || result.Value.Count() == dogs.Count()));
        }

        bool IsSorted(IEnumerable<DogDTO> dogs, Func<DogDTO, DogDTO, bool> compareFunc)
        {
            int length = dogs.Count();
            for(int i = 1;i < length; ++i)
            {
                if(!compareFunc(dogs.ElementAt(i-1), dogs.ElementAt(i)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
