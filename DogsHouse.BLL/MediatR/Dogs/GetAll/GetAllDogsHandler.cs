using AutoMapper;
using DogsHouse.BLL.DTO.Dogs;
using DogsHouse.BLL.MediatR.Dogs.Create;
using DogsHouse.DAL.Repositories.Wrapper;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouse.BLL.MediatR.Dogs.GetAll
{
    public class GetAllDogsHandler : IRequestHandler<GetAllDogQuery, Result<IEnumerable<DogDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoriesWrapper _repositoriesWrapper;

        public GetAllDogsHandler(IRepositoriesWrapper repositoriesWrapper, IMapper mapper)
        {
            _repositoriesWrapper = repositoriesWrapper;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<DogDTO>>> Handle(GetAllDogQuery request, CancellationToken cancellationToken)
        {
            return Result.Ok(_mapper.Map<IEnumerable<DogDTO>>(await _repositoriesWrapper.DogRepository.GetAllAsync()));
        }
    }
}
