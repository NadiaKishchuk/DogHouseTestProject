using AutoMapper;
using DogsHouse.BLL.DTO.Dogs;
using DogsHouse.DAL.Entities;
using DogsHouse.DAL.Repositories.Wrapper;
using FluentResults;
using MediatR;

namespace DogsHouse.BLL.MediatR.Dogs.Create
{
    public class CreateDogHandler : IRequestHandler<CreateDogQuery, Result<DogDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoriesWrapper _repositoriesWrapper;

        public CreateDogHandler(IRepositoriesWrapper repositoriesWrapper, IMapper mapper)
        {
            _repositoriesWrapper = repositoriesWrapper;
            _mapper = mapper;
        }
        public async Task<Result<DogDTO>> Handle(CreateDogQuery request, CancellationToken cancellationToken)
        {
            if(request.newDog.tail_length <= 0 || request.newDog.Weight <= 0)
            {
                return Result.Fail($"The dog tail length and weight cannot be less or equal then 0. Weight:{request.newDog.Weight}, tail length: {request.newDog.tail_length} ");

            }
            if (await _repositoriesWrapper.DogRepository.GetFirstOrDefaultAsync(x => x.Name == request.newDog.Name) != null)
            {
                return Result.Fail($"The dog with such name ({request.newDog.Name}) already exist");
            }
            Dog createdDog = await _repositoriesWrapper.DogRepository.CreateAsync(_mapper.Map<Dog>(request.newDog));

            await _repositoriesWrapper.SaveChangesAsync();

            return Result.Ok(_mapper.Map<DogDTO>(createdDog));
        }
    }
}
