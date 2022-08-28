using Discount.gRPC.Repository;
using Grpc.Core;

namespace Discount.gRPC.Services
{
    public class DiscounterService: Discounter.DiscounterBase
    {
        private readonly ICouponRepository couponRepository;

        public DiscounterService(ICouponRepository couponRepository)
        {
            this.couponRepository = couponRepository;
        }
        public override async Task<DiscountReply> GetDiscount(DiscountRequest discountRequest, ServerCallContext context)
        {
            var resp = (await couponRepository.GetDiscount(discountRequest.ProductName)).ToList();
            if (resp.Count < 1)
            {
                return new DiscountReply() { Discount = 0 };
            }
            return new DiscountReply() { Discount = resp[0].Amount };
        }
    }
}
