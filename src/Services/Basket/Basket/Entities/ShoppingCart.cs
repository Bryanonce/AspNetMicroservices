namespace Basket.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShppingCartItem> Items { get; set; } = new List<ShppingCartItem>();

        public ShoppingCart()
        {

        }

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach (var item in Items)
                {
                    totalprice += item.Price * item.Quantity;
                }
                return totalprice;
            }
        }
    }
}
