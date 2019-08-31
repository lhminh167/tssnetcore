using AutoMapper;
using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Mappings
{
    public class ProductFilterProfile : Profile
    {

    }

    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
		    CreateMap<ShoppingCart, ShoppingCartItemGet>();
            CreateMap<ShoppingCart, ShoppingCartItemAdd>();
        }
    }
}
