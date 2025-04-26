using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BasketServices(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var customerBasket = _mapper.Map< BasketDto ,CustomerBasket>(basket);
            var createdOrUpdatedBasket = _basketRepository.CreateOrUpdateBasketAsync(customerBasket);
            if (createdOrUpdatedBasket != null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("can not update or create basket now, try again later");

        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
            return await _basketRepository.DeleteBasketAsync(key);
        }

        public async Task<BasketDto> GetBasketAsync(string key)
        {
            var Basket = await _basketRepository.GetBasketAsync(key);
            if(Basket is not null)
                return _mapper.Map<CustomerBasket ,BasketDto>(Basket);
            else
                throw new BasketNotFoundExecption(key);
        }
    }
}
