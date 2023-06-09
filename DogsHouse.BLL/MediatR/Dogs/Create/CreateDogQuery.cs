using DogsHouse.BLL.DTO.Dogs;
using FluentResults;
using MediatR;

namespace DogsHouse.BLL.MediatR.Dogs.Create
{
    public record CreateDogQuery(DogDTO newDog) : IRequest<Result<DogDTO>>;
}
