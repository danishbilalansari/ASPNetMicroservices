using AutoMapper;
using Discount.Core.Entities;
using Discount.Grpc.Protos;

namespace Discount.Application.Mapper
{
    /// <summary>
    /// The ReverseMap() property is used for mapping coupon to coupon model and vice versa.
    /// </summary>
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
