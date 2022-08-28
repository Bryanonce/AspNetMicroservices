
using Discount.gRPC.Entities;

namespace Discount.gRPC.Repository
{
    public interface ICouponRepository
    {
        public Task<IEnumerable<Coupon>> GetDiscount(string productName);
        public Task<bool> CreateDiscount(Coupon coupon);
        public Task<bool> DeleteDiscount(string productName);
        public Task<bool> UpdateDiscount(Coupon coupon);
    }
}
