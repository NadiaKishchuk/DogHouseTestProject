using DogsHouse.BLL.DTO.Dogs;
using FluentResults;
using MediatR;

namespace DogsHouse.BLL.MediatR.Dogs.GetAll
{
    public record GetAllDogQuery : IRequest<Result<IEnumerable<DogDTO>>>;
}
