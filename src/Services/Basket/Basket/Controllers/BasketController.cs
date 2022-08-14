using Basket.Entities;
using Basket.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController:ControllerBase
    {
        private readonly IBasketRepository basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

        [HttpGet("{userName}", Name ="GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var shoppingCart = await basketRepository.GetBasket(userName);
            if(shoppingCart == null)
            {
                return NotFound();  
            }
            return shoppingCart;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart shoppingCart)
        {
            var basket = await basketRepository.UpdateBasket(shoppingCart);
            if (basket == null)
            {
                return BadRequest();
            }
            return basket;
        }

        [HttpDelete("{userName}")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteBasket(string userName)
        {
            return await basketRepository.DeleteBasket(userName);
        }
    }
}
