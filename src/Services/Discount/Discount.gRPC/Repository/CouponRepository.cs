using Discount.gRPC.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ApplicationDbContext context;

        public CouponRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Coupon>> GetDiscount(string productName)
        {
            return await context.Coupons.OrderByDescending(p => p.Id).Take(5).Where(p => p.ProductName == productName).ToListAsync();
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            context.Add(coupon);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            var coupon = await context.Coupons.FirstOrDefaultAsync(p => p.ProductName == productName);
            
            if(coupon == null)
            {
                return false;
            }

            context.Coupons.Remove(coupon);
            context.SaveChanges();

            return true;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            context.Coupons.Update(coupon);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
