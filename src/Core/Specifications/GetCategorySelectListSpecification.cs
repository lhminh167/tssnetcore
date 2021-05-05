using System.Collections.Generic;
using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Specifications
{
    public class GetCategorySelectListSpecification : BaseSpecification<Category, ProductFilterCategoryResponse>
    {
        public GetCategorySelectListSpecification()
        : base(
            c => true
            ,
            c => new ProductFilterCategoryResponse(c)
        )
        {
            SetIsNoTracking(true);
        }
    }

}
