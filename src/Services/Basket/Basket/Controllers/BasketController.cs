using Basket.API.Services;
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

        public ClientDiscountGrpc ClientDiscountGrpc { get; }
        public ILogger<BasketController> _Logger { get; }

        public BasketController(IBasketRepository basketRepository, ClientDiscountGrpc clientDiscountGrpc,  ILogger<BasketController> logger)
        {
            this.basketRepository = basketRepository;
            ClientDiscountGrpc = clientDiscountGrpc;
            _Logger = logger;
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
            foreach(var item in shoppingCart.Items)
            {
                var discount = 0;
                try
                {
                    discount = await ClientDiscountGrpc.GetDiscount(item.ProductName);
                } catch (Exception ex) {
                    _Logger.LogError(ex.Message);
                }
                item.Price -= discount;
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
