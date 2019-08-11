using AutoMapper;
using Tss.Core.ProductFilters;

namespace Tss
{
    public class AutoMapperConfiguration
    {
        public MapperConfiguration Configure() {
            return new MapperConfiguration(cfg => {
                cfg.AddProfile<ProductFiltersProductProfile>();
                cfg.ClearPrefixes();
                cfg.RecognizePrefixes("a", "av");
            });        
        }

        

    }
}