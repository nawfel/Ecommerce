using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Iterfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        public readonly IMapper _mapper;
        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _mapper = mapper;
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var updatedbasket = await _basketRepository.UpdateBasketAsync(_mapper.Map<CustomerBasketDto, CustomerBasket>(basket));
            return Ok(updatedbasket);
        }

        [HttpDelete]
        public async Task<ActionResult<CustomerBasket>> DelteBasketAsync(string id)
        {
            var updatedbasket = await _basketRepository.DeleteBasketAsync(id);
            return Ok(updatedbasket);
        }
    }
}