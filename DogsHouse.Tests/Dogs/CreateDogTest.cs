using AutoMapper;
using DogsHouse.BLL.DTO.Dogs;
using DogsHouse.BLL.MediatR.Dogs.Create;
using DogsHouse.DAL.Entities;
using DogsHouse.DAL.Repositories.Wrapper;
using DogsHouse.Tests.Utils;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace DogsHouse.Tests.Dogs
{
    public class CreateDogTest
    {
        private readonly Mock<IRepositoriesWrapper> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Dog createdDog;
        private readonly DogDTO createdDogDTO;
        public CreateDogTest()
        {
            _mockRepo = new Mock<IRepositoriesWrapper>();
            _mockMapper = new Mock<IMapper>();
            createdDog = new Dog() { Color = "brown", Name = "Day", TailLength = 3, Weight = 4 };
            createdDogDTO = new DogDTO()
            {
                Weight = createdDog.Weight,
                Name = createdDog.Name,
                Color = createdDog.Color,
                tail_length = createdDog.TailLength
            };
        }

        void SetupRepositoryCreate(Dog? dog)
        {
            _mockRepo.Setup(repo => repo.DogRepository.CreateAsync(It.IsAny<Dog>())).ReturnsAsync(createdDog);

            _mockRepo.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(1);
            _mockRepo.Setup(repo => repo.DogRepository.GetFirstOrDefaultAsync(
                  It.IsAny<Expression<Func<Dog, bool>>>(),
                It.IsAny<Func<IQueryable<Dog>, IIncludableQueryable<Dog, object>>>(),
                It.IsAny<Expression<Func<Dog, Dog>>>()))
                .ReturnsAsync(dog);
        }

        void SetupMapper()
        {
            _mockMapper.Setup(x => x.Map<IEnumerable<DogDTO>>(It.IsAny<IEnumerable<Dog>>()))
                .Returns<IEnumerable<Dog>>(arr => arr.Select(d =>
                new DogDTO() { Name = d.Name!, Color = d.Color!, tail_length = d.TailLength, Weight = d.Weight }));
            _mockMapper.Setup(x => x.Map<DogDTO>(It.IsAny<Dog>()))
                .Returns<Dog>(d => new DogDTO() { Name = d.Name!, Color = d.Color!, tail_length = d.TailLength, Weight = d.Weight });
        }
        [Fact]
        public async Task CreateDog_Handler_ReturnNewDogAsync()
        {
            SetupMapper();
            SetupRepositoryCreate(null);

            CreateDogHandler handler = new CreateDogHandler(_mockRepo.Object, _mockMapper.Object);

            var result = await handler.Handle(new CreateDogQuery(createdDogDTO), CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Multiple(
                () => Assert.True(result.Value is DogDTO),
                () => Assert.Equal(createdDogDTO, result.Value, new AllPropsDogEqualityComparer()));
        }

        [Fact]
        public async Task CreateDog_Handler_ReturnFail()
        {
            SetupMapper();
            SetupRepositoryCreate(createdDog);

            CreateDogHandler handler = new CreateDogHandler(_mockRepo.Object, _mockMapper.Object);

            var result = await handler.Handle(new CreateDogQuery(createdDogDTO), CancellationToken.None);

            Assert.False(result.IsSuccess);
        }

        [Theory]
        [InlineData(1, -1)]
        [InlineData(-1, -1)]
        [InlineData(-1, 1)]
        public async Task CreateDog_Handler_NegativeValues_ReturnFail(int taliLength, int weight)
        {
            DogDTO newDog = new DogDTO() { Color = "brown", Name = "Ola", tail_length = taliLength, Weight = weight };

            SetupMapper();
            SetupRepositoryCreate(createdDog);

            CreateDogHandler handler = new CreateDogHandler(_mockRepo.Object, _mockMapper.Object);

            var result = await handler.Handle(new CreateDogQuery(newDog), CancellationToken.None);

            Assert.False(result.IsSuccess);
        }

        [Theory]
        [InlineData("loooooooooooooooooooooong name more 30", "noramal value" )]
        [InlineData("loooooooooooooooooooooong name more 30", "loooooooooooooooooooooooooong color more 40 symbols")]
        [InlineData("normal value", "loooooooooooooooooooooooooong color more 40 symbols")]
        public void CreateDog_Handler_LongString_ReturnFail(string name , string color)
        {
            DogDTO newDog = new DogDTO() { Color = color, Name = name, tail_length = 2, Weight = 2 };

            SetupMapper();
            SetupRepositoryCreate(createdDog);

            CreateDogHandler handler = new CreateDogHandler(_mockRepo.Object, _mockMapper.Object);

            var func = async ()=> await handler.Handle(new CreateDogQuery(newDog), CancellationToken.None);

            Assert.ThrowsAsync<ArgumentException>(func);
        }
    }
}
