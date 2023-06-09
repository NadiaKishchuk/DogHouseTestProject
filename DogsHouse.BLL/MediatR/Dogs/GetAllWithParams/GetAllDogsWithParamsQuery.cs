using DogsHouse.BLL.DTO.AdditionalRequestDTO;
using DogsHouse.BLL.DTO.Dogs;
using FluentResults;
using MediatR;

namespace DogsHouse.BLL.MediatR.Dogs.GetAllWithParams
{
    public record GetAllDogsWithParamsQuery(string? attribute,
            OrderEnum? order, int? pageNumber, int? limit) : IRequest<Result<IEnumerable<DogDTO>>>;
}
