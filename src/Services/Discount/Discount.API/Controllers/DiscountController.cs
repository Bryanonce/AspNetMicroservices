using Discount.API.Entities;
using Discount.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController:ControllerBase
    {
        private readonly ICouponRepository _coupon;

        public DiscountController(ICouponRepository coupon)
        {
            this._coupon = coupon;
        }

        [HttpGet("{productName}")]
        public async Task<ActionResult<IEnumerable<Coupon>>> GetDiscount(string productName)
        {
            var available_coupons = await _coupon.GetDiscount(productName);
            return available_coupons.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateDiscount([FromBody] Coupon coupon)
        {
            return await _coupon.CreateDiscount(coupon);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateDiscount([FromBody] Coupon coupon)
        {
            return await _coupon.UpdateDiscount(coupon);
        }

        [HttpDelete("{productName}")]
        public async Task<ActionResult<bool>> DeleteDiscount(string productName)
        {
            return await _coupon.DeleteDiscount(productName);
        }
    }
}
