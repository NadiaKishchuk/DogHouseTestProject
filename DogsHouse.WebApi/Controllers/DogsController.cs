using DogsHouse.BLL.DTO.AdditionalRequestDTO;
using DogsHouse.BLL.DTO.Dogs;
using DogsHouse.BLL.MediatR.Dogs.Create;
using DogsHouse.BLL.MediatR.Dogs.GetAllWithParams;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.ComponentModel.DataAnnotations;

namespace DogsHouse.WebApi.Controllers
{
    public class DogsController: BaseController
    {
        [HttpGet]
        [Route("dogs")]
        public async Task<ActionResult> GetDogsParms(string? attribute, string? order,
            [Range(1, int.MaxValue)] int? pageNumber, [Range(1, int.MaxValue), FromQuery(Name = "limit=pageSize")] int? limit)
        { 
        
            OrderEnum? orderEnum=null;
            if (order!=null)
            {
                if(attribute != null)
                {
                    orderEnum = Enum.TryParse(order, true, out OrderEnum resOrder) ? resOrder : null;
                    if (orderEnum == null)
                    {
                        return NotFound($"The value of order param can be only {Enum.GetNames<OrderEnum>().Aggregate("", (res, x) => $"{res} {x}")}");
                    }
                }
                else
                {
                    return NotFound("no attribute for order was provided");
                }
            }

            return HandleResult(await Mediator
                .Send(new GetAllDogsWithParamsQuery(attribute, orderEnum, pageNumber,limit)));
        }

        [HttpPost]
        [Route("dog")]
        public async Task<ActionResult> Create(DogDTO dogDTO)
        {
            return HandleResult(await Mediator.Send(new CreateDogQuery(dogDTO)));
        }
    }
}
