using Discount.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.API
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }
    }
}
