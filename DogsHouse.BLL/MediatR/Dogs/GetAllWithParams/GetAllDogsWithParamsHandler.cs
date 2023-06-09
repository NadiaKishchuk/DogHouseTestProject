using AutoMapper;
using DogsHouse.BLL.DTO.AdditionalRequestDTO;
using DogsHouse.BLL.DTO.Dogs;
using DogsHouse.DAL.Entities;
using DogsHouse.DAL.Repositories.Wrapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace DogsHouse.BLL.MediatR.Dogs.GetAllWithParams
{
    public class GetAllDogsWithParamsHandler : IRequestHandler<GetAllDogsWithParamsQuery, Result<IEnumerable<DogDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoriesWrapper _repositoriesWrapper;

        public GetAllDogsWithParamsHandler(IRepositoriesWrapper repositoriesWrapper, IMapper mapper)
        {
            _repositoriesWrapper = repositoriesWrapper;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<DogDTO>>> Handle(GetAllDogsWithParamsQuery request, CancellationToken cancellationToken)
        {
            var dogs = _repositoriesWrapper.DogRepository.GetQueryable();
            if(request.attribute != null && request.order.HasValue)
            {
                string attribute = request.attribute;
                if (attribute.Equals("tail_length", StringComparison.OrdinalIgnoreCase))
                    attribute = "taillength";

                SortArray(ref dogs, attribute, request.order.Value);
            }
            if(request.limit != null && request.pageNumber != null)
            {
                TakePage(ref dogs, request.limit.Value, request.pageNumber.Value);
            }
            return Result.Ok( _mapper.Map<IEnumerable<DogDTO>>(await Task.FromResult(dogs.ToArray())));
        }

        private void TakePage(ref IQueryable<Dog> dogs, int limit , int pageNumber)
        {
            dogs = dogs.Skip((pageNumber - 1)*limit).Take(limit);
        }

        private void SortArray(ref IQueryable<Dog> dogs, string propertyName, OrderEnum order)
        {
            PropertyInfo? prop = typeof(Dog).GetProperty(propertyName,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            if (prop != null) {
                switch (order)
                {
                    case OrderEnum.Desc:
                        dogs = dogs.OrderByDescending(ToLambda<Dog>(prop.Name));
                        break;
                    case OrderEnum.Asc:
                        dogs = dogs.OrderBy(ToLambda<Dog>(prop.Name));
                        break;
                }
            }
        }
        private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);
            var propAsObject = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
        }
    }
}
