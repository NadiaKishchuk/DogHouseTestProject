using DogsHouse.BLL.DTO.Dogs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouse.BLL.MediatR.Dogs.Create
{
    public record CreateDogQuery(DogDTO newDog) : IRequest<Result<DogDTO>>;
}
