using Basket.Entities;

namespace Basket.Repository
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        Task<bool> DeleteBasket(string userName);
    }
}
