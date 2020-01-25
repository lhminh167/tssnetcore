using AutoMapper;
using Tss.Core.Models;

namespace Tss.Core.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<OrderCreateRequest, CustomerCreateRequest>();
        }
    }
}
