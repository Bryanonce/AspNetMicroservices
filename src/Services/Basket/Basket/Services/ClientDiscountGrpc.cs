using Discount.gRPC;

namespace Basket.API.Services
{
    public class ClientDiscountGrpc
    {
        private readonly Discounter.DiscounterClient _discounterClient;

        public ClientDiscountGrpc(Discounter.DiscounterClient discounterClient)
        {
            this._discounterClient = discounterClient;
        }

        public async Task<int> GetDiscount(string productName)
        {
            var discount = await _discounterClient.GetDiscountAsync(new DiscountRequest() { ProductName = productName });
            return discount.Discount;
        }
    }
}
